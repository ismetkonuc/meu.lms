using System.Collections;
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

        public List<AppUser> GetInstructors()
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
            }).Where(I => I.roles.Name == "Instructor").Select(I => new AppUser()
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

            var user = context.Users.Where(I => I.Email == email).First();

            return user;

        }

        public List<Course> GetUserCourses(int appUserId)
        {
            using var context = new LmsDbContext();

            var courses = context.Courses.ToList();
            var userCourseIds = context.CoursePeoples.Where(I => I.PersonId == appUserId).Select(I => I.CourseId).ToList();
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

        public List<Message> GetMessages(int ClientUserId, int TargetUserId)
        {
            using var context = new LmsDbContext();

            var messages = context.Messages.Where(I => (I.MessageTo == TargetUserId && I.AppUserId == ClientUserId) || (I.MessageTo == ClientUserId && I.AppUserId == TargetUserId)).ToList();

            return messages;
        }

        public List<AppUser> GetMessageList(int appUserId)
        {
            using var context = new LmsDbContext();

            Dictionary<int, List<int>> userMsg = new Dictionary<int, List<int>>()
            {
                [appUserId] = new List<int>()
            };

            var messages = context.Messages.Where(I => (I.AppUserId == appUserId) || (I.MessageTo == appUserId)).ToList();

            foreach (var message in messages)
            {
                userMsg[appUserId].Add(message.MessageTo);
                userMsg[appUserId].Add(message.AppUserId);
            }

            var userMessages = userMsg[appUserId].Distinct();


            List<AppUser> userChatList = new List<AppUser>();

            foreach (var user in userMessages)
            {
                if (user != appUserId)
                {
                    userChatList.Add(context.Users.Single(I => I.Id == user));
                }
            }


            return userChatList;
        }

        public List<AppUser> GetUserList()
        {
            using var context = new LmsDbContext();

            return context.Users.ToList();
        }
    }
}