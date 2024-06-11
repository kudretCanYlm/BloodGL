using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using BloodGL.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class MeasureController : ControllerBase
	{
		private readonly IGlucoseMeasureService glucoseMeasureService;

		public MeasureController(IGlucoseMeasureService glucoseMeasureService)
		{
			this.glucoseMeasureService = glucoseMeasureService;
		}

		[HttpGet, Route("GetMeasures")]
		public async Task<IActionResult> GetMeasures(string id)
		{
			var userId = id;
			var measures = await glucoseMeasureService.GetMeasures(userId);

			return Ok(measures);
		}

		[HttpPost,Route("PostMeasure")]
		public async Task<IActionResult> AddMeasure([FromBody] AddGlucoseMeasureDto addGlucoseMeasureDto)
		{
			var userId = addGlucoseMeasureDto.UserId;
			await glucoseMeasureService.Add(addGlucoseMeasureDto, userId);
			
			var res = new LoginResponseModel("a");
			return Ok(res);
		}

	}
}
