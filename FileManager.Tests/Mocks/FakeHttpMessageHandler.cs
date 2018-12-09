using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class FakeHttpMessageHandler : DelegatingHandler
    {
        private HttpResponseMessage _fakeResponse;

        public FakeHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _fakeResponse = responseMessage;
        }

        /// <summary>
        /// Creates a FakeHttpMessageHandler to be used with MockHttpClientFactory.
        /// Serialize content using JsonConvert.SerializeObject().
        /// </summary>
        /// <param name="contentToSerialize"></param>
        /// <param name="statusCode"></param>
        public FakeHttpMessageHandler(object contentToSerialize, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _fakeResponse = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(JsonConvert.SerializeObject(contentToSerialize), Encoding.UTF8, "application/json")
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            await Task.FromResult(_fakeResponse);
    }
}