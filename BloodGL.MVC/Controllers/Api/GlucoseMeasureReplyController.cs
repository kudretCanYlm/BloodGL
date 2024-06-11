﻿using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using BloodGL.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class ReplyController : ControllerBase
	{
		private readonly IUserDeviceService userDeviceService;

		public ReplyController(IUserDeviceService userDeviceService)
		{
			this.userDeviceService = userDeviceService;
		}

		[HttpPost]
		public async Task<IActionResult> AddDevice([FromBody]UserDeviceViewModel userDeviceViewModel)
		{
			var userId = userDeviceViewModel.UserId;
			await userDeviceService.Add(userId, userDeviceViewModel.Token);
			return Ok();
		}

		[HttpPost]
	
		public async Task<IActionResult> RemoveDevice([FromBody] UserDeviceViewModel userDeviceViewModel)
		{
			var userId = userDeviceViewModel.UserId;
			await userDeviceService.Add(userId, userDeviceViewModel.Token);

			return Ok();
		}
	}
}
