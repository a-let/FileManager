using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;

namespace FileManager.IntegrationTests
{
    public abstract class TestBase : IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        protected HttpClient _client;
        private readonly CustomWebApplicationFactory<Web.Startup> _factory;

        public TestBase(CustomWebApplicationFactory<Web.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        protected T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        protected StringContent CreateStringContent<T>(T value) =>
            new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}