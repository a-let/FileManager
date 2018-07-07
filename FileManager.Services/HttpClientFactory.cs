using System.Net.Http;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Client => new HttpClient();
    }
}