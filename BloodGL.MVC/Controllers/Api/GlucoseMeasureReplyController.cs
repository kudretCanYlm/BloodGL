using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using BloodGL.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class GlucoseMeasureReplyController : ControllerBase
	{
		private readonly IUserDeviceService userDeviceService;

		public GlucoseMeasureReplyController(IUserDeviceService userDeviceService)
		{
			this.userDeviceService = userDeviceService;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddDevice([FromBody]UserDeviceViewModel userDeviceViewModel)
		{
			var userId = HttpContext.GetUserId();
			await userDeviceService.Add(userId, userDeviceViewModel.Token);

			return Ok();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RemoveDevice([FromBody] UserDeviceViewModel userDeviceViewModel)
		{
			var userId = HttpContext.GetUserId();
			await userDeviceService.Add(userId, userDeviceViewModel.Token);

			return Ok();
		}
	}
}
