using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abaya.Models;

namespace Abaya.Controllers
{
    public class ProductsController : Controller
    {
        // قمنا بتغيير الاسم هنا ليتطابق مع مشروعك
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // جلب المنتجات من الجدول الموجود في قاعدة البيانات
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			// لازم نتأكد إن المنتج موجود في قاعدة البيانات ومعه رابط الصورة
			var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

			if (product == null) return NotFound();

			return View(product);
		}

		// 2. إضافة منتج جديد - GET
		public IActionResult Create()
        {
            return View();
        }

        // 2. إضافة منتج جديد - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // 3. تعديل منتج - GET
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // 3. تعديل منتج - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // 4. حذف منتج
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

