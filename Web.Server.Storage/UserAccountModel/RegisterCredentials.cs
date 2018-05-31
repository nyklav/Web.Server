using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Server.Storage
{
    /// <summary>
    /// The credentials to register on the server
    /// </summary>
    public class RegisterCredentials
    {
        /// <summary>
        /// The users username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The users pasword
        /// </summary>
        public string Password { get; set; }
    }
}
