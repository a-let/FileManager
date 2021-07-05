using FileManager.DataAccessLayer;
using FileManager.Web.Services.Interfaces;

using Logging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Text;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<FileManagerContext>();

            services.AddDbContext<FileManagerContext>(options =>
            {
                options.UseSqlServer(configuration["FileManagerConnectionString"]);
            });

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
                            logger.LogErrorAsync(context.Exception, $"Authentication Failed - Local: {localIp} Remote: {remoteIp}").GetAwaiter().GetResult();
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var localIp = context.HttpContext.Connection.LocalIpAddress.ToString();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
  
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();

                            // TODO: Handle null exception
                            logger.LogErrorAsync(context.AuthenticateFailure, $"Authentication Challenge - {context.Error} - Local: {localIp} Remote: {remoteIp}").GetAwaiter().GetResult();
                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            var localIp = context.HttpContext.Connection.LocalIpAddress.ToString();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                            logger.LogErrorAsync(context.Result.Failure, $"Authentication Forbidden - Local: {localIp} Remote: {remoteIp}").GetAwaiter().GetResult();
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
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileManager API", Version = "v1" });
                    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        },
                        In = ParameterLocation.Header,
                        Description = "Please enter JWT with bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
                            },
                            new string[] { }
                        }
                    });
                    c.ExampleFilters();
                })
                .AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }
    }
}