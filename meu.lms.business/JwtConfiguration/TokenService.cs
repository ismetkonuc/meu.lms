using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using meu.lms.entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace meu.lms.business.JwtConfiguration
{
    public class TokenService : IJwtFactory
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public string CreateJwt(AppUser appUser)
        {

            var claims = new List<Claim>()
            {
                //new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Token:Issuer"]),
                new Claim(ClaimTypes.Name , appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                NotBefore = DateTime.Now
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}