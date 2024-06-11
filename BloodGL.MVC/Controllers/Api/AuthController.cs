using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using BloodGL.Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]

	public class AuthController : ControllerBase
	{
		private readonly IUserAndAuthService userAndAuthService;
		private readonly ILoginDtoValidator loginDtoValidator;

		public AuthController(IUserAndAuthService userAndAuthService, ILoginDtoValidator loginDtoValidator)
		{
			this.userAndAuthService = userAndAuthService;
			this.loginDtoValidator = loginDtoValidator;
		}

		[HttpPost, Route("LoginMobile"),AllowAnonymous]
		public async Task<IActionResult> LoginMobile([FromBody]LoginDto model)
		{
			var result = loginDtoValidator.Validate(model);
			if (!result.IsValid)
			{
				return BadRequest("LoginDto is not valid");
			}

			var user = await userAndAuthService.LoginAndGetUser(model);

			if (user == null) { return BadRequest("username or password is invalid"); }

			if (user.Role == RoleEnum.Admin) { return BadRequest("you are not an user"); }

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Token));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
			identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });
			
			return Ok(new { token = user.Token });
		}
	}
}
