using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Web.Services;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Swagger;

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
                .AddScoped<ILogRepository, LogRepository>()
                .AddScoped<ILogger, LoggerService>()
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "FileManager API", Version = "v1" }))
                .AddDbContext<FileManagerContext>(o => o.UseSqlServer(_configuration["FileManagerConnectionString"], b=> b.MigrationsAssembly("FileManager.DataAccessLayer")))
                .AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            EnableSwagger(app);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void EnableSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileManger V1");
            });
        }
    }
}