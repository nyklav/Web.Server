using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Server.Storage
{
    /// <summary>
    /// The response for all calls made
    /// </summary>
    /// <typeparam name="T">response object</typeparam>
    public class Response<T>
    {
        // Verification the success of the request
        public bool Successful => ErrorMessage == null;
        // Message containing informaton about the error
        public string ErrorMessage { get; set; }
        // The response object
        public T Answer { get; set; }
    }
}
