using BloodGL.Application.Dtos;
using BloodGL.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BloodGL.Application
{
	public static class ApplicationIoc
	{
		public static void SetApplicationIoc(this WebApplicationBuilder builder)
		{
			builder.Services.AddAutoMapper(typeof(ApplicationMapperProfile));
			builder.Services.AddScoped(typeof(IUserAndAuthService), typeof(UserAndAuthService));
			builder.Services.AddScoped(typeof(IGlucoseMeasureService), typeof(GlucoseMeasureService));
			builder.Services.AddScoped(typeof(IGlucoseMeasureReplyService), typeof(GlucoseMeasureReplyService));
			builder.Services.AddScoped(typeof(IUserDeviceService), typeof(UserDeviceService));

			//Dtos vals
			builder.Services.AddScoped<ILoginDtoValidator, LoginDtoValidator>();
		}
	}
}
