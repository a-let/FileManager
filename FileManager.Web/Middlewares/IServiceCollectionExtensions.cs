using FileManager.DataAccessLayer;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                                logger.LogInformation("Login Failed");
                                context.Fail("Login Failed");
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

                            logger.LogError(context.Exception, "Authentication Failed - IP: {RemoteIp}", remoteIp);

                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

                            if (context.AuthenticateFailure == null)
                                logger.LogWarning("Authentication Challenge - IP: {RemoteIp}", remoteIp);
                            else
                                logger.LogError(context.AuthenticateFailure, "Authentication Challenge - IP: {RemoteIp}", remoteIp);

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

                            if (context.Result.Failure == null)
                                logger.LogWarning("Authentication Forbidden - IP: {RemoteIp}", remoteIp);
                            else
                                logger.LogError(context.Result.Failure, "Authentication Forbidden - IP: {RemoteIp}", remoteIp);

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

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
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