using BloodGL.Domain.Models;

namespace BloodGL.Application.Dtos
{
	public class UserDto
	{
		public string? Id { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public RoleEnum Role { get; set; }
		public string? Name { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
