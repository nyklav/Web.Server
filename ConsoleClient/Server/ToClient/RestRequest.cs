using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    /// <summary>
    /// Provider sending and receiving information from a HTTP server
    /// </summary>
    public static class RestRequest
    {
        // Client for communicating with the server
        private static RestClient _client = new RestClient();

        /// <summary>
        /// Posts a request to an URL and returns the HTTP response
        /// </summary>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="accessToken">The access token for authorization</param>
        /// <returns>The HTTP response</returns>
        public static async Task<HttpResponseMessage> PostDataAsync(
            string url,
            object content,
            string accessToken = null,
            string contentType = ContentType.Json)
        {
            // Add content type and token for authorization
            _client.AddAcceptHeader(contentType)
                   .AddBearerToken(accessToken);


            StringContent serializedObject = null;

            if ( content != null)
            {
                // Serialize content to Json
                serializedObject = new JsonContent(content);
            }

            // Return the server response
            return await _client.PostAsync(url, serializedObject, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
        }

        /// <summary>
        /// Posts a request to an URL and returns the processed HTTP request
        /// </summary>
        /// <typeparam name="T">The deserialization type</typeparam>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="accessToken">The access token for authorization</param>
        /// <param name="contentType">The content type</param>
        /// <returns></returns>
        public static async Task<RequestResult<T>> PostDataAsync<T>(
            string url,
            object content,
            string accessToken = null,
            string contentType = ContentType.Json)
        {
            var serverResponse = await PostDataAsync(url, content, accessToken, contentType);

            // Create a result
            var result = await serverResponse.CreateRequestResult<T>();

            // If the response status code is not 200
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = $"Server returned unsuccessful status code. {serverResponse.StatusCode} {serverResponse.ReasonPhrase}";
                return result;
            }

            // If the content is empty
            if (String.IsNullOrEmpty(result.ResponseContent))
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Content = JsonConvert.DeserializeObject<T>(result.ResponseContent);
            }
            catch (Exception ex)
            {
                // If deserialize failed then set error message
                result.ErrorMessage = "Failed to deserialize server response to the expected type";

                return result;
            }

            return result;
        }
    }
}
