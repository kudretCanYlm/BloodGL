using BloodGL.Domain.Models;

namespace BloodGL.Application.Dtos
{
	public class LoginJwtDto
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
		public RoleEnum Role { get; set; }
	}
}
