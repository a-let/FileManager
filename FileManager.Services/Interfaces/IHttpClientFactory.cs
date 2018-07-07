using System;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IHttpClientFactory
    {
        string BaseAddress { get; set; }
        Task<string> GetAsync(string requestUri);
        Task<string> PostAsync(object value, string requestUri);
        T DeserializeObject<T>(string value);
    }
}