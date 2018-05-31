using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Server.Storage;

namespace ConsoleClient
{
    public static class Account
    {
        public static async Task RegisterAsync()
        {
            var result = await Request.PostAsync<Response<RegisterResult>>(
                              "http://localhost:5000/signup",
                               new RegisterCredentials
                               {
                                   Username = "newUser",
                                   FirstName = "newFirstName",
                                   LastName = "newLastName",
                                   Email = "newUser@gmail.com",
                                   Password = "password"
                               });


            if (result == null || result.Content == null || result.Content.Successful)
            {
                if (result.Content != null)
                    Console.WriteLine(result.Content.ErrorMessage);
                else if (String.IsNullOrWhiteSpace(result.ResponseContent))
                    Console.WriteLine(result.ResponseContent);
                else if (result != null)
                    Console.WriteLine(result.ErrorMessage);
                else
                    Console.WriteLine("Unknown error");
            }
        }

        public static async Task LoginAsync()
        {
            var result = await Request.PostAsync<Response<LoginResult>>(
                              "http://localhost:5000/login",
                               new LoginCredentials
                               {
                                   UsernameOrEmail = "nyklav@gmail.com",
                                   Password = "password"
                               });


            if (result == null || result.Content == null || result.Content.Successful)
            {
                if (result.Content != null)
                    Console.WriteLine(result.Content.ErrorMessage);
                else if (String.IsNullOrWhiteSpace(result.ResponseContent))
                    Console.WriteLine(result.ResponseContent);
                else if (result != null)
                    Console.WriteLine(result.ErrorMessage);
                else
                    Console.WriteLine("Unknown error");
            }
        }
    }
}
