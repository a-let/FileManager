using FileManager.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace FileManager.IntegrationTests
{
    public class ShowTests : TestBase
    {
        public ShowTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async void Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Show");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<IEnumerable<Show>>(strContent);

            // Assert
            Assert.NotEmpty(shows);
        }
    }
}