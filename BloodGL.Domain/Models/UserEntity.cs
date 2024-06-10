using BloodGL.Core.Database.Entity;

namespace BloodGL.Domain.Models
{
	public class UserEntity:BaseEntity
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public RoleEnum Role { get; set; }
		public string? Name { get; set; }
		public string? PhoneNumber { get; set; }
	}

	public enum RoleEnum
	{
		User,
		Admin
	}
}
