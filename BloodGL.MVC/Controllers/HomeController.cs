using BloodGL.Application.Services;
using BloodGL.MVC.Helpers.Jwt;
using BloodGL.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BloodGL.MVC.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserAndAuthService userAndAuthService;
		private readonly IGlucoseMeasureService glucoseMeasureService;
		private readonly IGlucoseMeasureReplyService glucoseMeasureReplyService;
		private readonly IUserDeviceService userDeviceService;

		public HomeController(ILogger<HomeController> logger, IUserAndAuthService userAndAuthService, IGlucoseMeasureService glucoseMeasureService, IGlucoseMeasureReplyService glucoseMeasureReplyService, IUserDeviceService userDeviceService)
		{
			_logger = logger;
			this.userAndAuthService = userAndAuthService;
			this.glucoseMeasureService = glucoseMeasureService;
			this.glucoseMeasureReplyService = glucoseMeasureReplyService;
			this.userDeviceService = userDeviceService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Admin()
		{
			var users = await userAndAuthService.GetAll();

			return View(users);
		}

		public async Task<IActionResult> UserDetail(string id)
		{
			var user = await userAndAuthService.Get(id);
			if (user == null) { RedirectToAction("Admin"); }

			ViewBag.user = user;

			var glucoseMeasures = await glucoseMeasureService.GetMeasures(id);

			return View(glucoseMeasures);
		}
		public async Task<IActionResult> Reply(string id)
		{
			return View();
		}

		[HttpPost]
		//[Route("addreply/{ids}")]
		public async Task<IActionResult> AddReply(string id, [FromBody] ReplyViewModel replyViewModel)
		{
			var idsArray = id.Split(',');

			foreach (var idSingle in idsArray)
			{
				await glucoseMeasureReplyService.Add(idSingle, replyViewModel.Message);
			}

			return View();
		}

		public async Task<IActionResult> MeasureReplies(string id)
		{
			var replies = await glucoseMeasureReplyService.GetGlucoseMeasureReplies(id);
			var measure = await glucoseMeasureService.GetMeasure(id);

			ViewBag.measure = measure;

			return View(replies);
		}
	}
}