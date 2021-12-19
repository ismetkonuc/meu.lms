using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using meu.lms.entities.Concrete;
using Microsoft.IdentityModel.Tokens;

namespace meu.lms.business.JwtConfiguration
{
    public class JwtFactory : IJwtFactory
    {
        public string CreateJwt(AppUser appUser)
        {

            var claims = new List<Claim>()
            {
                new Claim("Email", appUser.Email),
                new Claim("Username", appUser.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));

            var token = new JwtSecurityToken(issuer: JwtInfo.Issuer, audience: JwtInfo.Audience, claims: claims,
                notBefore: DateTime.Now, expires: DateTime.Now.AddDays(7), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.CreateToken(tokenDescriptor);
            //string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            string tokenAsString = tokenHandler.WriteToken(token);


            return tokenAsString;

            //var claims = new List<Claim>()
            //{
            //    new Claim("Email", appUser.Email),
            //    new Claim("Username", appUser.Name)
            //};

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var tokenDescriptor = new SecurityTokenDescriptor()
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddDays(7),
            //    SigningCredentials = credentials,
            //    Issuer = JwtInfo.Issuer
            //};

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return tokenHandler.WriteToken(token);


        }
    }
}