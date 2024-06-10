using BloodGL.Core.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodGL.Domain.Models
{
	public class GlucoseMeasureEntity : BaseEntity
	{
		public int Measure { get; set; }
		public string UnitName { get { return "mg/Dl"; } }
		public ClucoseLevel Level { get; set; }
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public UserEntity User { get; set; }
	}

	public enum ClucoseLevel
	{
		Low,
		Medium,
		High
	}
}
