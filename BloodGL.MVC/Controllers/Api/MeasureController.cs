using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MeasureController : ControllerBase
	{
		private readonly IGlucoseMeasureService glucoseMeasureService;

		public MeasureController(IGlucoseMeasureService glucoseMeasureService)
		{
			this.glucoseMeasureService = glucoseMeasureService;
		}

		[HttpGet, Route("GetMeasures")]
		public async Task<IActionResult> GetMeasures()
		{
			var userId = HttpContext.GetUserId();
			var measures = await glucoseMeasureService.GetMeasures(userId);

			return Ok(measures);
		}

		[HttpPost,Route("PostMeasure")]
		public async Task<IActionResult> AddMeasure(AddGlucoseMeasureDto addGlucoseMeasureDto)
		{
			var userId = HttpContext.GetUserId();
			await glucoseMeasureService.Add(addGlucoseMeasureDto, userId);

			return Ok();
		}

	}
}
