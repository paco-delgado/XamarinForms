using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluToDoApp.Data.Helper
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return await _client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return await _client.PutAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _client.DeleteAsync(requestUri);
        }
    }
}
