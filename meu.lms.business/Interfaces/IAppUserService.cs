using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetStundets();
        AppUser GetCurrentUser(string email);
        List<Course> GetUserCourses(int appUserId);

    }
}