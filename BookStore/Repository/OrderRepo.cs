using BookStore.DAL;
using BookStore.Models;

namespace BookStore.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly BookStoreDbContext _context;

        public OrderRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> PlaceOrder(Order order, List<CartItem> cartItems)
        {
            decimal total = 0;

            var orderItems = new List<OrderItem>();

            foreach (var item in cartItems)
            {
                total += item.Book.Price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = item.Book.Price
                });
            }

            order.TotalAmount = total;
            order.OrderItems = orderItems;

            await _context.Orders.AddAsync(order);
            int res = await _context.SaveChangesAsync();

            // clear the cart items after placing the order
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return res;
        }
    }
}
