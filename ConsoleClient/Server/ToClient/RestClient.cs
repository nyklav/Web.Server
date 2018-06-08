using ConsoleClient.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class RestClient : RestClientBuilder
    {
        public RestClient()
            : base()
        { }

        public IDictionary<string, string> Headers
            => _client.DefaultRequestHeaders.ToDictionary(option => option.Key,option => option.Value.First());

        #region Send
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption option)
            => SendAsync(request, option, CancellationToken.None);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption option, CancellationToken cancellationToken)
        {
            return _client.SendAsync(request, option, cancellationToken);
        }
        #endregion

        // Post method
        #region Post
        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
            => SendAsync(new HttpRequestMessage(HttpMethod.Post, uri) { Content = content});

        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, HttpCompletionOption option)
            => SendAsync(new HttpRequestMessage(HttpMethod.Post, uri) { Content = content }, option);

        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken)
            => SendAsync(new HttpRequestMessage(HttpMethod.Post, uri) { Content = content }, cancellationToken);

        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, HttpCompletionOption option, CancellationToken cancellationToken)
            => _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri) { Content = content }, option, cancellationToken);
        
        #endregion
    }
}
