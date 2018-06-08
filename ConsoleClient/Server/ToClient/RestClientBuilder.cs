using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient.Server.Client
{
    public class RestClientBuilder : IDisposable
    {
        protected readonly HttpClient _client;

        public RestClientBuilder()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }

        public RestClientBuilder AddAcceptHeader(string type = ContentType.Json)
        {
            if (!String.IsNullOrWhiteSpace(type))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(type));
            }

            return this;
        }

        public RestClientBuilder AddDefaultHeaders(IDictionary<string, string> headers)
        {
            if (headers != null)
                foreach (var element in headers)
                {
                    _client.DefaultRequestHeaders.Add(element.Key, element.Value);
                }

            return this;
        }

        public RestClientBuilder AddBearerToken(string accessToken)
        {
            if (!String.IsNullOrWhiteSpace(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return this;
        }

        public RestClientBuilder AddBaseAddress(Uri baseAddress)
        {
            _client.BaseAddress = baseAddress;

            return this;
        }

        public RestClientBuilder AddBaseAddress(string baseAddress)
        {
            AddBaseAddress(new Uri(baseAddress));

            return this;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
