using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Web.Services;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                .AddScoped<IUserControllerService, UserControllerService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICryptographyService, CryptographyService>()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "FileManager API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", 
                        new ApiKeyScheme
                        {
                            In = "header",
                            Description = "Please enter JWT with Bearer into field",
                            Name = "Authorization",
                            Type = "apiKey"
                        });
                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", new string[] { } },
                    });
                    c.ExampleFilters();
                })
                .AddSwaggerExamplesFromAssemblyOf<Startup>()
                .AddDbContext<FileManagerContext>(o => o.UseSqlServer(_configuration["FileManagerConnectionString"], b=> b.MigrationsAssembly("FileManager.DataAccessLayer")))
                .AddMvc();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserControllerService>();
                        var userName = context.Principal.Identity.Name;
                        var user = userService.GetUserByUserName(userName);

                        if (user == null)
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                            logger.LogInformation("Login Failed");
                            context.Fail("Login Failed");
                        }                            

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                        logger.LogError(context.Exception, $"Authentication Failed - {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
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

            app.UseAuthentication();

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