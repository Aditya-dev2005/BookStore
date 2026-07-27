using BookStore.Models;

namespace BookStore.Repository
{
    public interface ICartRepo
    {
        Task<Cart> GetOrCreateCart(string sessionId);
        Task<List<CartItem>> GetCartItems(string sessionId);
        Task<int> AddToCart(string sessionId, int bookId, int quantity);
        Task<int> RemoveFromCart(int cartItemId);
        Task<int> UpdateQuantity(int cartItemId, int quantity);
        Task<int> ClearCart(string sessionId);
    }
}
