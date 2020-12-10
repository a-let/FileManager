using FileManager.Web.Services.Interfaces;

using Logging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration["FileManagerConnectionString"], name: "FileManager Database Check", tags: new[] { "filemanagerdb" });

            services.AddHealthChecksUI();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(x =>
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
                            // TODO: Make awaitable if possible.
                            var user = userService.GetAsync(userName).Result;

                            if (user == null)
                            {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                                logger.LogInfoAsync("Login Failed");
                                context.Fail("Login Failed");
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var localIp = context.HttpContext.Connection.LocalIpAddress.ToString();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                            logger.LogErrorAsync(context.Exception, $"Authentication Failed - Local: {localIp} Remote: {remoteIp}");
                            return Task.CompletedTask;
                        }
                    };

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services
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
                .AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }
    }
}