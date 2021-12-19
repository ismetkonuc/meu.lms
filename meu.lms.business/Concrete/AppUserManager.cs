using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private IAppUserDal _appUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }
        public List<AppUser> GetStundets()
        {
            return _appUserDal.GetStundets();
        }

        public AppUser GetCurrentUser(string email)
        {
           return _appUserDal.GetCurrentUser(email);
        }

        public List<Course> GetUserCourses(int appUserId)
        {
            return _appUserDal.GetUserCourses(appUserId);
        }
    }
}