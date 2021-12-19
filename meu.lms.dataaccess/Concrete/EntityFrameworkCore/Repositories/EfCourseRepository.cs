using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCourseRepository : EfGenericRepository<Course>, ICourseDal
    {
        public List<Task> GetCourseTasks(int courseId)
        {
            using var db = new LmsDbContext();

            return db.Tasks.Where(I => I.Id == courseId).ToList();

            //return db.Courses.Single(I => I.Id == courseId).Tasks;
        }

        public Course GetCourseWithId(int courseId)
        {
            using var db = new LmsDbContext();

            return db.Courses.Single(I => I.Id == courseId);
        }

        public List<Course> GetAll()
        {
            using var db = new LmsDbContext();

            return db.Courses.ToList();
        }
    }
}