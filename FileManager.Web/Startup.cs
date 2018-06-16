﻿using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}