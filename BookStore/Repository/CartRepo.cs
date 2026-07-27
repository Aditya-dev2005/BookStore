using Microsoft.EntityFrameworkCore;
using BookStore.DAL;
using BookStore.Models;

namespace BookStore.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly BookStoreDbContext _context;

        public CartRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetOrCreateCart(string sessionId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CreatedDate = DateTime.Now
                };
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<List<CartItem>> GetCartItems(string sessionId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);

            if (cart == null)
            {
                return new List<CartItem>();
            }

            return await _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.CartId == cart.CartId)
                .ToListAsync();
        }

        public async Task<int> AddToCart(string sessionId, int bookId, int quantity)
        {
            var cart = await GetOrCreateCart(sessionId);

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.BookId == bookId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    BookId = bookId,
                    Quantity = quantity
                };
                await _context.CartItems.AddAsync(cartItem);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCart(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null)
            {
                return 0;
            }
            _context.CartItems.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateQuantity(int cartItemId, int quantity)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null)
            {
                return 0;
            }
            item.Quantity = quantity;
            _context.CartItems.Update(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ClearCart(string sessionId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);

            if (cart == null)
            {
                return 0;
            }

            var items = await _context.CartItems
                .Where(ci => ci.CartId == cart.CartId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);
            return await _context.SaveChangesAsync();
        }
    }
}
