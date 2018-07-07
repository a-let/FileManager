using System.Net.Http;

namespace FileManager.Services.Interfaces
{
    public interface IHttpClientFactory
    {
        HttpClient Client { get; }
    }
}