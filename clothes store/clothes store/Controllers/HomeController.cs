using clothes_store.Models;
using clothes_store.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace clothes_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Project context;
        private readonly ProductRepository products;
        public List<Product> prods;
        private readonly IProduct _product;
        public HomeController(ILogger<HomeController> logger,ProductRepository prod,Project p, IProduct product)
        {
            _logger = logger;
            products = prod;
            context = p;
            _product = product;
        }


        public IActionResult Search(string SearchByName)
        {
           
            var Products = from n in context.Products
                           select n;

            if (!string.IsNullOrEmpty(SearchByName))
            {
                // Filter products from the database context based on the search query
                Products = Products.Where(n => n.Name.Contains(SearchByName));

                // Pass the filtered list of products to the view
                return View(Products.ToList());
            }

            // If no search query is provided, return an empty list
            return View(Products.ToList());
        }

        public IActionResult aboutus()
        {
            return View();
        }
        public IActionResult Shippinginfo()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ProductPopupPartial(int productId)
        {
            // Fetch product details based on the product ID
            var product = _product.GetbyId(productId);

            // Return the partial view with the product details
            return PartialView("_ProductPopupPartial", product);
        }
        public IActionResult PaymentMethod()
        {
            return View();
        }
        public IActionResult feedback()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}