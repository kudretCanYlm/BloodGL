using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using BloodGL.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class GlucoseMeasureReplyController : ControllerBase
	{
		private readonly IUserDeviceService userDeviceService;

		public GlucoseMeasureReplyController(IUserDeviceService userDeviceService)
		{
			this.userDeviceService = userDeviceService;
		}

		[HttpPost]
		public async Task<IActionResult> AddDevice(UserDeviceViewModel userDeviceViewModel)
		{
			var userId = HttpContext.GetUserId();
			await userDeviceService.Add(userId, userDeviceViewModel.Token);

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> RemoveDevice(UserDeviceViewModel userDeviceViewModel)
		{
			var userId = HttpContext.GetUserId();
			await userDeviceService.Add(userId, userDeviceViewModel.Token);

			return Ok();
		}
	}
}
