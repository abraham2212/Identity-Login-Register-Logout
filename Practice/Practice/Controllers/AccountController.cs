using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.ViewModels.Account;

namespace Practice.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager,
									SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			try
			{
				if (!ModelState.IsValid) return View(model);

				AppUser newUser = new()
				{
					FullName = model.FullName,
					Email = model.Email,
					UserName = model.Username,
					IsRememberMe = model.IsRememberMe
				};

				IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(model);
				}

				//await _signInManager.SignInAsync(newUser,model.IsRememberMe);

				return RedirectToAction(nameof(Login));
			}
			catch (Exception ex)
			{
				ViewBag.error = ex.Message;
				return View();
			}

		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM model)
		{
			try
			{
				if (!ModelState.IsValid) return View(model);

				var user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);

				if (user == null)
				{
					user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
				}
				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Email or password is wrong");
					return View(model);
				}

				var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);
			
				if (!res.Succeeded)
				{
					ModelState.AddModelError(string.Empty, "Email or password is wrong");
					return View(model);
				}

				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ViewBag.error = ex.Message;
				return View();
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
