using ProductService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SdaProductService.Repositories;
using System.Text;

namespace ProductService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddTransient((config) =>
			{
				var cfg = new JWTConfiguration();
				Configuration.GetSection("JWTConfiguration").Bind(cfg);
				return cfg;
			});


			services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

			services.AddControllers();
			ConfigureJWT(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		public void ConfigureJWT(IServiceCollection services)
		{
			var config = services.BuildServiceProvider().GetService<JWTConfiguration>();
			
			var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.SecretKey));

			var tokenValidtionParameters = new TokenValidationParameters
			{
				IssuerSigningKey = signingKey,
				ValidIssuer = config.Issuer,
				ValidAudience = config.ValidAudience,
			};

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(c =>
			{
				c.RequireHttpsMetadata = false;
				c.SaveToken = true;
				c.TokenValidationParameters = tokenValidtionParameters;
			});
		}
	}
}
