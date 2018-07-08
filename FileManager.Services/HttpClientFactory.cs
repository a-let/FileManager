using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public string BaseAddress { get; set; }

        public T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        public async Task<string> GetAsync(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUri);

                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
        }

        public async Task<string> PostAsync(object value, string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, content);

                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
        }
    }
}