using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Server.Storage
{
    /// <summary>
    /// Client credentials to log into the server
    /// </summary>
    public class LoginCredentials
    {
        // The user username or email 
        public string UsernameOrEmail { get; set; }
        // The user passsword
        public string Password { get; set; }
    }
}
