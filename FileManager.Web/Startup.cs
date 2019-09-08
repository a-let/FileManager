using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Web.Middlewares;
using FileManager.Web.Services;
using FileManager.Web.Services.Interfaces;

using HealthChecks.UI.Client;

using Logging;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace FileManager.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IEpisodeControllerService, EpisodeControllerService>()
                .AddScoped<IEpisodeRepository, EpisodeRepository>()
                .AddScoped<ISeasonControllerService, SeasonControllerService>()
                .AddScoped<ISeasonRepository, SeasonRepository>()
                .AddScoped<ISeriesControllerService, SeriesControllerService>()
                .AddScoped<ISeriesRepository, SeriesRepository>()
                .AddScoped<IShowControllerService, ShowControllerService>()
                .AddScoped<IShowRepository, ShowRepository>()
                .AddScoped<IMovieControllerService, MovieControllerService>()
                .AddScoped<IMovieRepository, MovieRepository>()
                .AddScoped<IUserControllerService, UserControllerService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICryptographyService, CryptographyService>()
                .AddScoped<ITokenGenerator, TokenGenerator>()
                .AddDbContext<FileManagerContext>(o => o.UseSqlServer(_configuration["FileManagerConnectionString"], b=> b.MigrationsAssembly("FileManager.DataAccessLayer")))
                .AddMvc();

            services.ConfigureLogging(Assembly.GetEntryAssembly().GetName().Name);
            services.AddCustomHealthChecks(_configuration);
            services.AddCustomAuthentication(_configuration);
            services.AddCustomSwagger(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler();

            if (!env.IsProduction())
                app.EnableSwagger();

            app.UseStaticFiles();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(config => config.UIPath = "/health-ui");

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