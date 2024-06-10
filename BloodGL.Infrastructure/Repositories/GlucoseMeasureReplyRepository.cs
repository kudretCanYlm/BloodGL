using BloodGL.Core.Database.Options;
using BloodGL.Core.Database.Repository;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Options;

namespace BloodGL.Infrastructure.Repositories
{
	public class GlucoseMeasureReplyRepository : RepositoryBase<GlucoseMeasureReplyEntity>, IGlucoseMeasureReplyRepository
	{
		public GlucoseMeasureReplyRepository(IOptions<MongoDbOptions> options) : base(options)
		{
		}
	}
}
