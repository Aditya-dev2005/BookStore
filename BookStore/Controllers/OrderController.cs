using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartRepo _cartRepo;
        private readonly IOrderRepo _orderRepo;
        private const string SessionKey = "CartSessionId";

        public OrderController(ICartRepo cartRepo, IOrderRepo orderRepo)
        {
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
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
        public async Task<IActionResult> Checkout()
        {
            string sessionId = GetSessionId();
            List<CartItem> items = await _cartRepo.GetCartItems(sessionId);

            if (items == null || items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            ViewBag.CartItems = items;
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            string sessionId = GetSessionId();
            List<CartItem> items = await _cartRepo.GetCartItems(sessionId);

            if (items == null || items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            order.SessionId = sessionId;
            order.OrderDate = DateTime.Now;
            order.Status = "Confirmed";

            await _orderRepo.PlaceOrder(order, items);

            return RedirectToAction("Confirmation", new { id = order.OrderId });
        }

        [HttpGet]
        public IActionResult Confirmation(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
