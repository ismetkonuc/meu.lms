using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;
using meu.lms.entities.Enums;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfTaskRepository : EfGenericRepository<Task>, ITaskDal
    {
        public List<Task> GetActiveTasks()
        {
            using var db = new LmsDbContext();

            return db.Tasks.Where(I => I.Status == Status.Active).ToList();
        }

        public List<Task> GetTasksWithCourseId(int courseId)
        {
            using var db = new LmsDbContext();


            return db.Tasks.Where(I => I.CourseId == courseId).ToList();
        }
    }
}