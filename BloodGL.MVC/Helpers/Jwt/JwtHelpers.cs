using System.Security.Claims;

namespace BloodGL.MVC.Helpers.Jwt
{
	public static class JwtHelpers
	{
		public static string GetUserId(this HttpContext httpContext)
		{
			return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
		}
	}
}
