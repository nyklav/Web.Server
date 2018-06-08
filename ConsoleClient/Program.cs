using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Server.Storage;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
       
        static async Task MainAsync()
        {
            var result = await RestRequest.PostDataAsync<Response<LoginResult>>(
                              "http://localhost:5000/login",
                               new LoginCredentials
                               {
                                   UsernameOrEmail = "nyklav@gmail.com",
                                   Password = "password"
                               });

            Console.ReadLine();
        }
    }
}
