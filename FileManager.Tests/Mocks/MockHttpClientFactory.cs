using System.Threading.Tasks;

using FileManager.Services.Interfaces;

namespace FileManager.Tests.Mocks
{
    public class MockHttpClientFactory : IHttpClientFactory
    {
        public string BaseAddress { get; set; }

        public T DeserializeObject<T>(string value)
        {
            return default(T);
        }

        public Task<string> GetAsync(string requestUri)
        {
            return Task.FromResult("Test Get");
        }

        public Task<string> PostAsync(object value, string requestUri)
        {
            return Task.FromResult("Test Post");
        }
    }
}