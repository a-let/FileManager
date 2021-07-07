using FileManager.Models.Dtos;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.IntegrationTests
{
    public abstract class TestBase : IClassFixture<CustomWebApplicationFactory<Web.Startup>>
    {
        protected HttpClient _client;

        public TestBase(CustomWebApplicationFactory<Web.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        protected T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        protected StringContent CreateStringContent<T>(T value) =>
            new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        protected async Task<string> GetToken(string userName, string password)
        {
            var message = await _client.PostAsync("api/User/Authenticate", CreateStringContent(new UserDto { UserName = userName, Password = password }));
            var messageDict = DeserializeObject<Dictionary<string, string>>(await message.Content.ReadAsStringAsync());
            var token = messageDict["token"];
            return token;
        }
    }
}