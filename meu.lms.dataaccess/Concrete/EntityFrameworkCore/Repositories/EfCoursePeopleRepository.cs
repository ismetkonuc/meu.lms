using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoursePeopleRepository : EfGenericRepository<CoursePeople>, ICoursePeopleDal
    {
        public List<Course> GetUserCourses(int userId)
        {
            using var db = new LmsDbContext();

            var courseIds= db.CoursePeoples.Where(I => I.PersonId == userId).Select(I => I.CourseId).ToList();

            List<Course> userCourses = new List<Course>();

            foreach (var courseId in courseIds)
            {
                userCourses.Add(db.Courses.Single(I=>I.Id==courseId));   
            }

            return userCourses;
            //return db.Assignments.Where(I => I.Id == taskId).ToList();

        }
    }
}