using clothes_store.Models;
using clothes_store.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clothes_store.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
      
        [HttpGet]
        public async Task<IActionResult> login()
        {
            var loginVm = new LoginVm();

            return View(loginVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await UserManager.FindByNameAsync(loginVm.Name);
                if (applicationUser != null)
                {
                    bool found=await UserManager.CheckPasswordAsync(applicationUser, loginVm.Password);
                    if (found == true)
                    {
                        await SignInManager.SignInAsync(applicationUser,loginVm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Name Or PAssword Wrong");
            }
            return View(loginVm);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newuser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usermodel = new ApplicationUser();
                usermodel.UserName = newuser.UserNAme;
                usermodel.Address = newuser.Address;
                usermodel.Email = newuser.Email;
                usermodel.PhoneNumber = newuser.PhoneNumber;
                usermodel.PasswordHash = newuser.Password;
                IdentityResult result = await UserManager.CreateAsync(usermodel, newuser.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(usermodel, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var erroritem in result.Errors)
                    {
                        ModelState.AddModelError("Password", erroritem.Description);
                       
                    }
                }
            }
            return View(newuser);
           
            
        }
        
        public async Task<IActionResult> logout()
        {
           
             await SignInManager.SignOutAsync();
            return RedirectToAction("login");
        }
        public IActionResult ExternalLogin(string provider, string returnUrl = "")
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string remoteError = "")
        {
            if (!string.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError("", $"Error from external login provider: {remoteError}");
                return View("Login", new LoginVm());
            }

            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", "External login information is not available.");
                return View("Login", new LoginVm());
            }

            var signInResult = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return SafeRedirect(returnUrl);
            }

            var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = await UserManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        Address = "Default",
                        EmailConfirmed = true

                    };
                    var result = await UserManager.CreateAsync(newUser);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to create user.");
                        return View("Login", new LoginVm());
                    }
                    user = newUser; // Assign the newly created user
                }

                await SignInManager.SignInAsync(user, isPersistent: false);
                return SafeRedirect(returnUrl);
            }

            ModelState.AddModelError("", "User email is not available from external provider.");
            return View("Login", new LoginVm());
        }

        private IActionResult SafeRedirect(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterViewModel newuser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usermodel = new ApplicationUser();
                usermodel.UserName = newuser.UserNAme;
                usermodel.Email = newuser.Email;
                usermodel.Address=newuser.Address;
                usermodel.PhoneNumber = newuser.PhoneNumber;
                usermodel.PasswordHash = newuser.Password;
                IdentityResult result = await UserManager.CreateAsync(usermodel, newuser.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(usermodel,"Admin");
                    await SignInManager.SignInAsync(usermodel, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var erroritem in result.Errors)
                    {
                        ModelState.AddModelError("Password", erroritem.Description);

                    }
                }
            }
            return View(newuser);

        }

    }
}
