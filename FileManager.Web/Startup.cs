using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
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
                .AddScoped<IFileManagerObjectAdapter<Episode>, EpisodeAdapter>()
                .AddScoped<ISeasonControllerService, SeasonControllerService>()
                .AddScoped<IFileManagerObjectAdapter<Season>, SeasonAdapter>()
                .AddScoped<ISeriesControllerService, SeriesControllerService>()
                .AddScoped<IFileManagerObjectAdapter<Series>, SeriesAdapter>()
                .AddScoped<IShowControllerService, ShowControllerService>()
                .AddScoped<IFileManagerObjectAdapter<Show>, ShowAdapter>()
                .AddScoped<IMovieControllerService, MovieControllerService>()
                .AddScoped<IFileManagerObjectAdapter<Movie>, MovieAdapter>()
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