using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Server.Storage;

namespace Web.Server.Controllers
{
    public class AccountController : Controller
    {
        protected UserAccountDbContext _userAccountDbContext;
        protected UserManager<UserAccount> _userManager;
        protected SignInManager<UserAccount> _signInManager;


        public AccountController(
            UserAccountDbContext userAccountDbContext,
            UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager)
        {
            _userAccountDbContext = userAccountDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create()
        {
            //_userAccountDbContext.Database.EnsureCreated();

            //var result = await _userManager.CreateAsync(new UserAccount
            //{
            //    UserName = "nyklav",
            //    FirstName = "Nyk",
            //    LastName = "Lav",
            //    Email = "nyklav@gmail.com"
            //}, "password");

            //if (result.Succeeded)
                return Content("User created", "text/html");

            //return Content("Faild", "text/html");
        }

        /// <summary>
        /// Register a new account on the server
        /// </summary>
        /// <param name="registerCredentials">Information for registration</param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<Response<RegisterResult>> RegisterAsync([FromBody]RegisterCredentials registerCredentials)
        {
            var errorValidation = new Response<RegisterResult>
            {
                ErrorMessage = "Correctly fill in all dields of registretion data."
            };

            if  (registerCredentials == null)
            {
                return errorValidation;
            }

            if (String.IsNullOrWhiteSpace(registerCredentials.Username) ||
                String.IsNullOrWhiteSpace(registerCredentials.Email) ||
                String.IsNullOrWhiteSpace(registerCredentials.Password))
            {
                return errorValidation;
            }

            var user = new UserAccount
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email
            };

            // Created a user
            var result = await _userManager.CreateAsync(user, registerCredentials.Password);

            if (!result.Succeeded)
            {
                // Return the failed response
                return new Response<RegisterResult>
                {
                    // Aggregate all error
                    ErrorMessage = result.Errors?.ToList()
                        .Select(o => o.Description)
                        .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")
                };
            }

            return new Response<RegisterResult>
            {
                Answer = new RegisterResult
                {
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        /// <summary>
        /// Authorization of the user on the server
        /// </summary>
        /// <param name="loginCredentials">Information for authentication</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<Response<LoginResult>> LogInAsync([FromBody]LoginCredentials loginCredentials)
        {
            var errorValidation = new Response<LoginResult>
            {
                ErrorMessage = "Invalid login or password"
            };

            if (String.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message
                return errorValidation;

            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            var user = isEmail ?
                await _userManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                await _userManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            if (user == null)
                return errorValidation;

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginCredentials.Password);

            if (!isValidPassword)
                return errorValidation;

            return new Response<LoginResult>
            {
                Answer = new LoginResult
                {
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = user.GenerateJwtToken()
                }
            };
        }
    }
}