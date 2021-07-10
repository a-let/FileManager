using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Middlewares;
using FileManager.Web.Services;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SimpleInjector;

namespace FileManager.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FileManagerContext>(optionsBuilder =>
            {
                optionsBuilder
                    .UseSqlServer(_configuration["FileManagerConnectionString"], b => b.MigrationsAssembly("FileManager.DataAccessLayer"));
            });

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddCustomHealthChecks(_configuration);
            services.AddCustomAuthentication(_container, _configuration);
            services.AddCustomSwagger();

            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();
            });

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            _container.Register(typeof(IControllerService<>), typeof(IControllerService<>).Assembly);
            _container.Register(typeof(IRepository<>), typeof(IRepository<>).Assembly);

            _container.Register<ICryptographyService, CryptographyService>();
            _container.Register<ITokenGenerator, TokenGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            app.UseCustomRequestLogging(_container);

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

            _container.Verify();
        }
    }
}