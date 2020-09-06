using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostgreExample.Domain.Contracts;
using PostgreExample.Domain.Services;
using PostgreExample.Infrastructure.Data;
using PostgreExample.Infrastructure.Data.Repositories;

namespace PostgreExample.Api
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
			services.AddControllers();

			services.AddSingleton<ICardsService, CardsService>();

			services.Configure<PostgreSqlOptions>(Configuration.GetSection("PostgreSql"));

			services.AddSingleton<ICardsRepository, CardsRepository>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
