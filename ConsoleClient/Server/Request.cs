using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    /// <summary>
    /// Provider sending and receiving information from a HTTP server
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Posts a request to an URL and returns the HTTP response
        /// </summary>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="accessToken">The access token for authorization</param>
        /// <returns>The HTTP response</returns>
        public static async Task<HttpWebResponse> PostAsync(string url, object content = null, string accessToken = null)
        {
            // Create the HttpWebRequest
            var webRequest = WebRequest.CreateHttp(url);
            // Set the post method
            webRequest.Method = HttpMethod.Post.ToString();
            // Content type
            webRequest.ContentType = "application/json";
            // Return type
            webRequest.Accept = "application/json";

            if (accessToken != null)
            {
                /// Adding access token for authentication
                webRequest.PreAuthenticate = true;
                // Add to header collection the authorization with a token
                webRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
            }

            if (content == null)
            {
                webRequest.ContentLength = 0;
            }
            else
            {
                // Serialize content to Json
                var serializedObject = JsonConvert.SerializeObject(content);

                // Body stream 
                using (var requestStream = await webRequest.GetRequestStreamAsync())
                {
                    // Create a stream entry
                    using (var streamWriter = new StreamWriter(requestStream))
                    {
                        // Write content to HTTP body
                        await streamWriter.WriteAsync(serializedObject);
                    }
                }
            }

            // Return the server response
            return await webRequest.GetResponseAsync() as HttpWebResponse;
        }

        /// <summary>
        /// Posts a request to an URL and returns the processed HTTP request
        /// </summary>
        /// <typeparam name="T">The deserialization type</typeparam>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="accessToken">The access token for authorization</param>
        /// <returns>The processed HTTP request</returns>
        public static async Task<RequestResult<T>> PostAsync<T>(string url, object content = null, string accessToken = null)
        {
            var serverResponse = await PostAsync(url, content, accessToken);

            // Create a result
            var result = await serverResponse.CreateRequestResult<T>();

            // If the response status code is not 200
            if (result.StatusCode != HttpStatusCode.OK)
            {
                // Call failed
                result.ErrorMessage = $"Server returned unsuccessful status code. {serverResponse.StatusCode} {serverResponse.StatusDescription}";
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