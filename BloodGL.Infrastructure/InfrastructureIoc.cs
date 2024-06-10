using BloodGL.Domain.Interfaces;
using BloodGL.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BloodGL.Infrastructure
{
	public static class InfrastructureIoc
	{
		public static void SetInfrastructureIoc(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped(typeof(IUserAndAuthRepository), typeof(UserAndAuthRepository));
			builder.Services.AddScoped(typeof(IGlucoseMeasureRepository), typeof(GlucoseMeasureRepository));
			builder.Services.AddScoped(typeof(IGlucoseMeasureReplyRepository), typeof(GlucoseMeasureReplyRepository));
			builder.Services.AddScoped(typeof(IUserDeviceRepository), typeof(UserDeviceRepository));
		}
	}
}
