using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using BloodGL.Core.Database.Firebase;
using BloodGL.MVC.Helpers.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodGL.MVC.Controllers
{
    [Authorize]
    public class MeasureController : Controller
    {
        private readonly IGlucoseMeasureService glucoseMeasureService;
        private readonly FirebaseNotification firebaseNotification; 
        public MeasureController(IGlucoseMeasureService glucoseMeasureService, FirebaseNotification firebaseNotification)
        {
            this.glucoseMeasureService = glucoseMeasureService;
            this.firebaseNotification = firebaseNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetUserId();
            var result = await glucoseMeasureService.GetMeasures(userId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            //var userId = HttpContext.GetUserId();
            //var testMeasure = new AddGlucoseMeasureDto
            //{
            //    Measure = 90
            //};

            //await glucoseMeasureService.Add(testMeasure, userId);
            //await firebaseNotification.SendNotification();

            return Ok();
        }
    }
}
