using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Server
{
    public static class JwtBearer
    {
        /// <summary>
        /// Extension method for <see cref="UserAccount"/>
        /// Generate a token for user using a data account
        /// </summary>
        /// <param name="user">Data on the user account</param>
        /// <returns></returns>
        public static string GenerateJwtToken(this UserAccount user)
        {
            var claims = new[]
            {
                // THe unique identificator
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // Generate a security key
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: IoCContainer.Configuration["Jwt:Issuer"],
                audience: IoCContainer.Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
