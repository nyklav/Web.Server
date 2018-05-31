  namespace Web.Server.Storage
{
    /// <summary>
    /// Result of successful login
    /// </summary>
    public class LoginResult
    {
        // The user username
        public string Username { get; set; }
        // The user first name
        public string FirstName { get; set; }
        // The user last name
        public string LastName { get; set; }
        // The user email
        public string Email { get; set; }
        // The authentication token
        public string Token { get; set; }
    }
}
