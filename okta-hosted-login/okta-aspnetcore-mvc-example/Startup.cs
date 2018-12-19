using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaxDeductionReporting
{
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.AspNetCore.Authentication.OpenIdConnect;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.IdentityModel.Protocols.OpenIdConnect;
	using Microsoft.IdentityModel.Tokens;
	using Okta.Sdk;
	using Okta.Sdk.Configuration;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			IConfigurationSection openIdOptions = Configuration.GetSection("Okta");

			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
				})
			.AddCookie()
			.AddOpenIdConnect(options =>
				{
					options.ClientId = openIdOptions["ClientId"];
					options.ClientSecret = openIdOptions["ClientSecret"];
					options.Authority = openIdOptions["Issuer"];
					options.CallbackPath = "/authorization-code/callback";
					options.ResponseType = OpenIdConnectResponseType.Code;
					options.SaveTokens = true;
					options.UseTokenLifetime = false;
					options.GetClaimsFromUserInfoEndpoint = true;
					options.Scope.Add("openid");
					options.Scope.Add("profile");
					options.TokenValidationParameters = new TokenValidationParameters
					{
						NameClaimType = "name"
					};
				});

			services.AddSingleton<IOktaClient>
			(
				new OktaClient(new OktaClientConfiguration
				{
					OktaDomain = openIdOptions["OktaDomain"],
					Token = openIdOptions["APIToken"]
				})
			);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}


	}
}
