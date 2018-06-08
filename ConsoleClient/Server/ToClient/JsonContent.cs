using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ConsoleClient
{
    class JsonContent : StringContent
    {
        /// <summary>
        /// Creating the string content from an serialized object
        /// </summary>
        /// <param name="conent">The object to serialize</param>
        /// <param name="mediaType">The content type</param>
        public JsonContent(object conent, string mediaType = ContentType.Json)
            : base(JsonConvert.SerializeObject(conent), Encoding.UTF8, mediaType)
        { }
    }
}
