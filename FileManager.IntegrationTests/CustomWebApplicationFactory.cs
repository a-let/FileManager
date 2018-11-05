using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FileManager.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Web.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }
}