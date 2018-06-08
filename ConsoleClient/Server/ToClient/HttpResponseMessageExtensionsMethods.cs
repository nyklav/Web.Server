using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public static class HttpResponseMessageExtensionsMethods
    {
        /// <summary>
        /// Extensions method for <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="webResponse">Original web response</param>
        /// <returns></returns>
        public static async Task<RequestResult<T>> CreateRequestResult<T>(this HttpResponseMessage response)
        {
            // New web request result
            var result = new RequestResult<T>
            {
                StatusCode = response.StatusCode,
                StatusDescription = response.ReasonPhrase,
                ContentType = response.RequestMessage.Headers.Accept.ToString()
            };

            // If the response status code is 200
            if (response.IsSuccessStatusCode)
            {
                //Writing content to a string
                result.ResponseContent = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
