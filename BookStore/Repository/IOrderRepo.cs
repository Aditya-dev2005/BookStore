using BookStore.Models;

namespace BookStore.Repository
{
    public interface IOrderRepo
    {
        Task<int> PlaceOrder(Order order, List<CartItem> cartItems);
    }
}
