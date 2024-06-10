using BloodGL.Domain.Models;
using BloodGL.Domain.Enums;
using BloodGL.Core.Database.Repository;

namespace BloodGL.Domain.Interfaces
{
	public interface IGlucoseMeasureRepository : IBaseRepository<GlucoseMeasureEntity>
	{
		Task Add(GlucoseMeasureEntity glucoseMeasureEntity);
		Task<IEnumerable<GlucoseMeasureEntity>> GetGlucoseMeasures(string userId);
		Task<IEnumerable<GlucoseMeasureEntity>> GetGlucoseMeasuresByDate(string userId, MeasureTimeIntervalEnum time);
	}
}
