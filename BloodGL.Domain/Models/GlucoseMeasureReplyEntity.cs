using BloodGL.Core.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodGL.Domain.Models
{
	public class GlucoseMeasureReplyEntity:BaseEntity
	{
		public string Reply { get; set; }
		public string GlucoseMeasureId { get; set; }

		[ForeignKey("GlucoseMeasureId ")]
		public GlucoseMeasureEntity User { get; set; }
	}
}
