using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Web.Middlewares;
using FileManager.Web.Services;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

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
                .AddDbContext<FileManagerContext>(o => o.UseSqlServer(_configuration["FileManagerConnectionString"], b => b.MigrationsAssembly("FileManager.DataAccessLayer")));

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddCustomHealthChecks(_configuration);
            services.AddCustomAuthentication(_configuration);
            services.AddCustomSwagger(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            app.UseCustomExceptionHandler();

            if (!env.IsProduction())
                app.EnableSwagger();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}