using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Interfaces;
using FileManager.BusinessLayer.Repositories;
using FileManager.Models;
using FileManager.Web.Services;

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
                .AddScoped<IFileManagerDb, FileManagerDb>()
                .AddScoped<IEpisodeControllerService, EpisodeControllerService>()
                .AddScoped<IFileManagerObjectRepository<Episode>, EpisodeRepository>()
                .AddScoped<ISeasonControllerService, SeasonControllerService>()
                .AddScoped<IFileManagerObjectRepository<Season>, SeasonRepository>()
                .AddScoped<ISeriesControllerService, SeriesControllerService>()
                .AddScoped<IFileManagerObjectRepository<Series>, SeriesRepository>()
                .AddScoped<IShowControllerService, ShowControllerService>()
                .AddScoped<IFileManagerObjectRepository<Show>, ShowRepository>()
                .AddScoped<IMovieControllerService, MovieControllerService>()
                .AddScoped<IFileManagerObjectRepository<Movie>, MovieRepository>()
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "FileManager API", Version = "v1" }))
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