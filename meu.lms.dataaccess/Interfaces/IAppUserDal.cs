using System.Collections.Generic;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace meu.lms.dataaccess.Interfaces
{
    public interface IAppUserDal
    {
        List<AppUser> GetStundets();
        AppUser GetCurrentUser(string email);
        List<Course> GetUserCourses(int appUserId);
        List<Message> GetMessages(int ClientUserId, int TargetUserId);
        List<AppUser> GetMessageList(int appUserId);
        List<AppUser> GetUserList();
        List<AppUser> GetInstructors();
    }
}