using BloodGL.Core.Database.Options;
using BloodGL.Core.Database.Repository;
using BloodGL.Domain.Enums;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Options;

namespace BloodGL.Infrastructure.Repositories
{
	public class GlucoseMeasureRepository : RepositoryBase<GlucoseMeasureEntity>, IGlucoseMeasureRepository
	{
		public GlucoseMeasureRepository(IOptions<MongoDbOptions> options) : base(options)
		{

		}

		public async Task<IEnumerable<GlucoseMeasureEntity>> GetGlucoseMeasures(string userId)
		{
			return await GetMany(x => x.UserId == userId);
		}

		public async Task<IEnumerable<GlucoseMeasureEntity>> GetGlucoseMeasuresByDate(string userId, MeasureTimeIntervalEnum time)
		{
			
			double days = 0;

			switch (time)
			{
				case MeasureTimeIntervalEnum.Today: days = -1; break;
				case MeasureTimeIntervalEnum.LastWeek: days = -7; break;
				case MeasureTimeIntervalEnum.LastMonth: days = -30; break;
				case MeasureTimeIntervalEnum.LastYear: days = -360; break;
			}

			var date=DateTime.Now.AddDays(days);

			return await GetMany(x => x.UserId == userId && x.CreatedAt.Value >= date);
		}
	}
}
