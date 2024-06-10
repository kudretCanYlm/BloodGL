using BloodGL.Domain.Models;

namespace BloodGL.Application.Dtos
{
	public class AddUserDto
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Name { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
