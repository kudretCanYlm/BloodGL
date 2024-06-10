using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using FluentValidation.AspNetCore;
using BloodGL.Domain.Models;

namespace BloodGL.MVC.Controllers
{

	public class AuthController : Controller
	{
		private readonly IUserAndAuthService userAndAuthService;
		private readonly ILoginDtoValidator loginDtoValidator;

		public AuthController(IUserAndAuthService userAndAuthService, ILoginDtoValidator loginDtoValidator)
		{
			this.userAndAuthService = userAndAuthService;
			this.loginDtoValidator = loginDtoValidator;
		}

		public IActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated) { return Redirect("/Home/Admin"); }

			return View();
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return RedirectToAction("Login");
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(AddUserDto addUserDto)
		{
			await userAndAuthService.AddUser(addUserDto, RoleEnum.Admin);
			return View();
		}

		[Authorize(Roles = "Admin")]
		public IActionResult RegisterNewUser()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RegisterNewUser(AddUserDto addUserDto)
		{
			await userAndAuthService.AddUser(addUserDto);
			return View();
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Test()
		{
			return Ok("deneme 13");
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginDto model, string returnUrl = null)
		{
			var result = loginDtoValidator.Validate(model);
			if (!result.IsValid)
			{
				result.AddToModelState(this.ModelState);
				return View("Login");
			}

			ViewData["ReturnUrl"] = returnUrl;
			if (ModelState.IsValid)
			{
				var user = await userAndAuthService.LoginAndGetUser(model);

				if (user == null)
				{
					ModelState.AddModelError("", "username or password is invalid");
					return View(model);
				}

				if (user.Role == RoleEnum.User)
				{
					ModelState.AddModelError("", "you are not an admin");
					return View(model);
				}

				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Token));
				identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
				identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

				return RedirectToLocal("login");
			}

			return View(model);
		}

		[NonAction]
		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Admin", "Home");
			}
		}
	}
}
