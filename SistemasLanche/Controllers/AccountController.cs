using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemasLanche.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SistemasLanche.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel()
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (!ModelState.IsValid)
				return View(loginVM);

			var user = await _userManager.FindByNameAsync(loginVM.UserName);

			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user,
					loginVM.Password, false, false);

				if (result.Succeeded)
				{
					if (string.IsNullOrEmpty(loginVM.ReturnUrl))
					{
						return RedirectToAction("Index", "Home");
					}
					return Redirect(loginVM.ReturnUrl);
				}
			}
			ModelState.AddModelError("", "Falha ao realizar o login!!");
			return View(loginVM);
		}//

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(LoginViewModel registroVM)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = registroVM.UserName };
				var result = await _userManager.CreateAsync(user, registroVM.Password);

				if (result.Succeeded)
				{
					//await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Login", "Account");
				}
				else
				{
					this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
				}
			}
			return View(registroVM);
		}

		[HttpPost]
		public async Task<IActionResult> Logout() 
		{
			HttpContext.Session.Clear(); //Zera todos os métodos da session
			HttpContext.User = null; //Zera todos os dados do User
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
