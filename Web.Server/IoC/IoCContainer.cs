using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server
{
    public static class IoCContainer
    {
        public static ServiceProvider Provider { get; set; }
        /// <summary>
        /// Configuration manager
        /// </summary>
        public static IConfiguration Configuration { get; set; }
    }
}
