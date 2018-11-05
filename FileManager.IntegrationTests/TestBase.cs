using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace FileManager.IntegrationTests
{
    public abstract class TestBase : IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        protected HttpClient _client;

        public TestBase(CustomWebApplicationFactory<Web.Startup> factory)
        {
            _client = factory.CreateClient();
        }
    }
}