using BloodGL.Core.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodGL.Domain.Models
{
	public class UserDeviceEntity:BaseEntity
	{
		public string Token { get; set; }
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public UserEntity User { get; set; }
	}
}
