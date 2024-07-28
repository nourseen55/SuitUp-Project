using clothes_store.Models;
using clothes_store.Repository;
using clothes_store.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes_store.Controllers
{
    public class ProductController : Controller
    {
        private readonly Project _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProduct _product;

        public ProductController(Project context, IWebHostEnvironment hostEnvironment, IProduct _product)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            this._product = _product;

        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new ProductVM()
            {
                Categories = _context.Categories.ToList(),
                lCategories = _context.LargeCategories.ToList()
            };
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(ProductVM productvm, IFormFile urlimg)
        {

            var wwwroot = _hostEnvironment.WebRootPath + "/img/gallery/";
            Guid id = Guid.NewGuid();
            string fullpath = Path.Combine(wwwroot, id + urlimg.FileName);
            using (var filestream = new FileStream(fullpath, FileMode.Create))
            {
                urlimg.CopyTo(filestream);
            }
            productvm.urlimg = id + urlimg.FileName;

            var product = new Product
            {
                Name = productvm.Name,
                Price = productvm.Price,
                Category_id = productvm.Category_id,
                urlimg = productvm.urlimg,
                lCategory_id = productvm.lCategory_id,
            };
            _context.Products.Add(product);
            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction("Index"); // Redirect to the Index action
        }

        public IActionResult Index()
        {
            var item = _product.GetAll();
            return View(item);
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var cat = _product.GetbyId(id);
            if (cat == null) return NotFound();
            return View(cat);
        }
        public IActionResult DetailsName(string name)
        {
            var cat = _product.GetbyName(name);
            if (cat == null) return NotFound();
            return View("Details", cat);
        }
        [Authorize]
        public async Task<IActionResult> ToggleFavorite([FromBody] ToggleFavoriteViewModel model)
        {
            try
            {
                var product = await _context.Products.FindAsync(model.ProductId);

                if (product == null)
                {
                    return NotFound();
                }

                product.IsFavorite = model.IsFavorite;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize]

        public async Task<IActionResult> showfav()
        {
            var favoriteProducts = await _context.Products.Where(p => p.IsFavorite == true).ToListAsync();
            return View(favoriteProducts);
        }
        public IActionResult ShowProducts(int categoryId)
        {
            var products = _context.Products.Where(p => p.Category_id == categoryId).ToList();
            return View("womenpro", products);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {

            var product = _product.GetbyId(id);
            if (product == null) return NotFound();
            ProductVM newpro = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Categories = _context.Categories.ToList(),
                Category_id = product.Category_id,
                urlimg = product.urlimg,
                lCategory_id = product.lCategory_id,
                lCategories = _context.LargeCategories.ToList(),
            };

            return View(newpro);
        }
        [HttpPost]
        public IActionResult Edit(ProductVM productVM, IFormFile urlimg)

        {
            var product = _product.GetbyId(productVM.Id);
            if (product == null) return NotFound();
            product.Name = productVM.Name;
            product.Price = productVM.Price;
            product.Category_id = productVM.Category_id;
            product.lCategory_id = productVM.lCategory_id;
            var files = Request.Form.Files;
            if (files.Any())
            {
                var wwwroot = _hostEnvironment.WebRootPath + "/img/gallery/";
                Guid id = Guid.NewGuid();
                string fullpath = Path.Combine(wwwroot, id + urlimg.FileName);
                using (var filestream = new FileStream(fullpath, FileMode.Create))
                {
                    urlimg.CopyTo(filestream);
                }
                productVM.urlimg = id + urlimg.FileName;
            }
            product.urlimg = productVM.urlimg;

            _context.Entry(product).State = EntityState.Modified;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _product.GetbyId(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var product = _product.GetbyId(id);
            if (product == null) return NotFound();
            _product.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult IndexAdmin()
        {
            var item = _product.GetAll();
            return View(item);
        }


    }
}
