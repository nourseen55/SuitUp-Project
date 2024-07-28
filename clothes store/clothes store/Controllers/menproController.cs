using clothes_store.Models;
using clothes_store.Repository;
using Microsoft.AspNetCore.Mvc;

namespace clothes_store.Controllers
{
    public class menproController : Controller
    {
        private readonly Project _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProduct _product;

        public menproController(Project context, IWebHostEnvironment hostEnvironment, IProduct _product)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            this._product = _product;

        }
        public IActionResult ShowProducts(int categoryId)
        {
            var products = _context.Products.Where(p => p.Category_id == categoryId).ToList();
            return View("productmen", products);
        }
    }
}
