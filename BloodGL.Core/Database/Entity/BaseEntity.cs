using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace BloodGL.Core.Database.Entity
{
	public abstract class BaseEntity : IBaseEntity
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Guid? DeletedBy { get; set; }
		public Guid? ModifiedBy { get; set; }
		public Guid? CreatedBy { get; set; }
		public bool isDeleted { get; set; }

		//[BsonElement("items")]
		//[JsonPropertyName("items")]
		//public List<string> movieIds { get; set; } = null!;
	}
}
