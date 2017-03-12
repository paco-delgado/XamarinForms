using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluToDoApp.Data.Helper
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
    }
}