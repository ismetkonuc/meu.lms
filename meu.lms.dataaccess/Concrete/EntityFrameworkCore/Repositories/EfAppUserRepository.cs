using System.Collections.Generic;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal
    {
        public List<AppUser> GetStundets()
        {
            using var context = new LmsDbContext();

            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Student").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                Surname = I.user.Surname,
                Email = I.user.Email,

            }).ToList();
        }

        public AppUser GetCurrentUser(string email)
        {
            using var context = new LmsDbContext();

            var user = context.Users.Where(I=>I.Email == email).First();

            return user;

        }

        public List<Course> GetUserCourses(int appUserId)
        {
            using var context = new LmsDbContext();

            var courses = context.Courses.ToList();
            var userCourseIds = context.CoursePeoples.Where(I=>I.PersonId == appUserId).Select(I=>I.CourseId).ToList();
            List<Course> userCourses = new List<Course>();

            foreach (var course in courses)
            {
                if (userCourseIds.Where(I => I == course.Id).Any())
                {
                    userCourses.Add(course);
                }
            }


            return userCourses;

        }
    }
}