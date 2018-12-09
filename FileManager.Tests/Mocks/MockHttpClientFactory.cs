using System.Net.Http;

namespace FileManager.Tests.Mocks
{
    public class MockHttpClientFactory : IHttpClientFactory
    {
        public FakeHttpMessageHandler FakeHttpMessageHandler { get; set; }

        public HttpClient CreateClient(string name)
        {
            return new HttpClient(FakeHttpMessageHandler)
            {
                BaseAddress = new System.Uri("http://good.uri")
            };
        }
    }
}