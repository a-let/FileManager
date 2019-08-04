using Newtonsoft.Json;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; }

        public ServiceBase(IHttpClientFactory httpClientFactory, string clientName)
        {
            Client = httpClientFactory.CreateClient(clientName);
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var obj = await SendAsync<T>(requestMessage);
            return obj;
        }

        protected async Task<T> PostAsync<T>(string url, object payload)
        {
            var jsonPayload = SerializeObject(payload);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = CreateHttpContent(jsonPayload)
            };

            var obj = await SendAsync<T>(requestMessage);
            return obj;
        }

        /// <summary>
        /// Default creates new StringContent with Encoding.UTF8 and application/json.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected virtual HttpContent CreateHttpContent(string content) =>
            new StringContent(content, Encoding.UTF8, "application/json");

        /// <summary>
        /// Deserializes to object of type T using JsonConvert.DeserializeObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        /// <summary>
        /// Serializes to string using JsonConver.SerializeObject.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        protected string SerializeObject(object payload) => JsonConvert.SerializeObject(payload);

        private async Task<T> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            var responseMessage = await Client.SendAsync(requestMessage);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var obj = DeserializeObject<T>(content);
            return obj;
        }
    }
}