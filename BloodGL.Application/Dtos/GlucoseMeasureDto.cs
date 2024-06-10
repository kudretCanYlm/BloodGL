using BloodGL.Domain.Models;

namespace BloodGL.Application.Dtos
{
	public class GlucoseMeasureDto
	{
		public string? Id { get; set; }
		public int Measure { get; set; }
		public string UnitName { get { return "mg/Dl"; } }
		public ClucoseLevel Level { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
