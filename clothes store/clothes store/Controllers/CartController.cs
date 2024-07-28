using clothes_store.Models;
using clothes_store.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes_store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly Project context;
        private readonly IUserCart _userCart;
        private readonly IProduct _product;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartController(IUserCart userCart, IProduct product, UserManager<ApplicationUser> userManager, Project context)
        {
            _userCart = userCart;
            _product = product;
            _userManager = userManager;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // Get current user
            var cartItem = _userCart.GetCartItems(user.Id); // Pass user ID
            return View(cartItem);
        }

        public async Task<IActionResult> AddToCart(int ProductId)
        {
            var user = await _userManager.GetUserAsync(User); // Get current user
            _userCart.AddToCart(ProductId, user.Id); // Pass user ID
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> DecreaseFromCart(int ProductId)
        //{
        //    var user = await _userManager.GetUserAsync(User); // Get current user
        //    _userCart.DecreaseFromCart(ProductId, user.Id);
        //    return RedirectToAction("Index", "Cart", new { id = ProductId });
        //}
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            _userCart.RemoveFromCart(productId, user.Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var user = await _userManager.GetUserAsync(User);
            _userCart.ClearCart(user.Id);

            return Ok();
        }
        
        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int quantity)
        {
            var item = context.Items.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                item.Quantity = quantity;
                context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}
