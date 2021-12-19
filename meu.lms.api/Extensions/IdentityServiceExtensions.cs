using System;
using System.Text;
using meu.lms.business.JwtConfiguration;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
namespace meu.lms.api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            //var builder = services.AddIdentityCore<AppUser>();
            //builder = new IdentityBuilder(builder.UserType, builder.Services);
            //builder.AddEntityFrameworkStores<LmsDbContext>();
            //builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = configuration["Token:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };
            });

            return services;


        }
    }
}