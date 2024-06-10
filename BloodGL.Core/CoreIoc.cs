using BloodGL.Core.Database.Firebase;
using BloodGL.Core.Database.Options;
using BloodGL.Core.Database.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BloodGL.Core
{
	public static class CoreIoc
	{
		public static void SetCoreIoc(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(typeof(MongoDbOptions).Name));
			builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(RepositoryBase<>));
			builder.Services.AddScoped<FirebaseNotification>();
		}
	}
}
