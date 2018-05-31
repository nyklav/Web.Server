using System.Net;

namespace ConsoleClient
{
    public class RequestResult<T>
    {
        /// <summary>
        /// Checking for a successful call
        /// </summary>
        public bool Successful => ErrorMessage != null;
        /// <summary>
        /// If something failed
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// The status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// The status description
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// The response headers
        /// </summary>
        public WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// The type of content returned
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// The response body
        /// </summary>
        public string ResponseContent { get; set; }
        /// <summary>
        /// The deserializable object
        /// </summary>
        public T Content { get; set; }
    }
}