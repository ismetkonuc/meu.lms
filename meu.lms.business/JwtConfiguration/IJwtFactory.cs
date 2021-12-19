using System.Collections.Generic;
using System.Security.Claims;
using meu.lms.entities.Concrete;

namespace meu.lms.business.JwtConfiguration
{
    public interface IJwtFactory
    {
        string CreateJwt(AppUser appUser);
    }
}