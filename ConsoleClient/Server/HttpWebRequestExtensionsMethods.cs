using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public static class HttpWebRequestExtensionsMethods
    {
        /// <summary>
        /// Extensions method for <see cref="HttpWebRequest"/>
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="webResponse">Original web response</param>
        /// <returns></returns>
        public static async Task<RequestResult<T>> CreateRequestResult<T>(this HttpWebResponse webResponse)
        {
            // New web request result
            var result = new RequestResult<T>
            {
                StatusCode = webResponse.StatusCode,
                StatusDescription = webResponse.StatusDescription,
                //Headers = webResponse.Headers,
                ContentType = webResponse.ContentType
            };

            // If the response status code is 200
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                // Open the response stream
                using (var stream = webResponse.GetResponseStream())
                {
                    // Get a stream reader
                    using (var streamReader = new StreamReader(stream))
                    {
                        // Read the response body
                        result.ResponseContent = await streamReader.ReadToEndAsync();
                    }
                }
            }

            return result;
        }
    }
}
