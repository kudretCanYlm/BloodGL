using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BloodGL.Core;
using BloodGL.Application;
using BloodGL.Infrastructure;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();
		builder.SetCoreIoc();
		builder.SetApplicationIoc();
		builder.SetInfrastructureIoc();

		var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("key") ?? "");

		builder.Services.AddAuthentication(auth =>
		{
			auth.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			auth.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(jwt =>
		{
			jwt.RequireHttpsMetadata = false;
			jwt.SaveToken = true;
			jwt.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false

			};
		})
		.AddCookie(options =>
		{
			options.LoginPath = "/auth/login";
			options.LogoutPath = "/auth/logout";
		});

		var app = builder.Build();


		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Admin}/{id?}");

		app.Run();
	}
}