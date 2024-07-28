using clothes_store.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clothes_store.Controllers
{
	[Authorize(Roles ="Admin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> roleManager1;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
			   roleManager1 = roleManager;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult New()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> New(RoleViewModel roleView)
		{
			if(ModelState.IsValid==true)
			{
				IdentityRole role=new IdentityRole();
				role.Name = roleView.RoleName;
				IdentityResult result=await roleManager1.CreateAsync(role);
				if(result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}

			}
			
			return View(roleView);
		}

	}
}
