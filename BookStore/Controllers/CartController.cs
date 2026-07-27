using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepo _cartRepo;
        private const string SessionKey = "CartSessionId";

        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString(SessionKey, sessionId);
            }
            return sessionId;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string sessionId = GetSessionId();
            List<CartItem> items = await _cartRepo.GetCartItems(sessionId);
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity = 1)
        {
            string sessionId = GetSessionId();
            await _cartRepo.AddToCart(sessionId, bookId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartRepo.RemoveFromCart(cartItemId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            if (quantity < 1)
            {
                await _cartRepo.RemoveFromCart(cartItemId);
            }
            else
            {
                await _cartRepo.UpdateQuantity(cartItemId, quantity);
            }
            return RedirectToAction("Index");
        }
    }
}
