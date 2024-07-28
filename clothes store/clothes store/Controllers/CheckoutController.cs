using clothes_store.Models;
using clothes_store.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace clothes_store.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly Icheckout _checkout;
        private readonly IUserCart _userCart;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(Icheckout icheckout, IUserCart userCart, UserManager<ApplicationUser> userManager)
        {
            _checkout = icheckout;
            _userCart = userCart;
            _userManager = userManager;
        }
        public async Task<IActionResult> ThankYou()
        {
            var user = await _userManager.GetUserAsync(User);
            var cartItem = _userCart.GetCartItems(user.Id);
            return View(cartItem);
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutData checkoutData)
        {
            if (ModelState.IsValid)
            {
                _checkout.Add(checkoutData);
                return RedirectToAction("ThankYou");
            }

            return View("Checkout");
        }

    }
}
