using Abaya.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Abaya.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // 1. عرض السلة
        public IActionResult Index()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }

        // 2. إضافة منتج للسلة
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var cart = GetCartFromSession();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++; // إذا موجود بنزيد الكمية
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        // 3. حذف عنصر من السلة
        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        // --- ميثود مساعدة للتعامل مع الـ Session ---
        private List<CartItem> GetCartFromSession()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            return sessionData == null
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionData);
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            var sessionData = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", sessionData);
        }
    }
}
