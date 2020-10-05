using Contracts;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DesafioEdCRUD.Services
{
    public class IdentityService : IidentityService
    {
        private readonly IConfiguration _config;
        public IdentityService(IConfiguration config)
        {
            _config = config;
        }

        public string BuildJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _config["JwtToken:Issuer"];
            var audience = _config["JwtToken:Audience"];
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtToken:TokenExpiry"]));

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool Authenticate(Login login)
        {
            if (login.Username == _config["AuthLogin:UserName"] && login.Password == _config["AuthLogin:PassWord"])
            {
                return true;
            }
            return false;
        }
    }
}
