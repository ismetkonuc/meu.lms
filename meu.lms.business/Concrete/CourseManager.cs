using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class CourseManager : ICourseService
    {

        private readonly ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public void Save(Course table)
        {
            _courseDal.Save(table);
        }

        public void Delete(Course table)
        {
            _courseDal.Delete(table);
        }

        public void Update(Course table)
        {
            _courseDal.Update(table);
        }

        public Course GetTaskWithId(int id)
        {
            return _courseDal.GetTaskWithId(id);
        }

        public List<Course> GetAllTasks()
        {
            return _courseDal.GetAllTasks();
        }

        public List<Task> GetCourseTasks(int courseId)
        {
            return _courseDal.GetCourseTasks(courseId);
        }

        public Course GetCourseWithId(int courseId)
        {
            return _courseDal.GetCourseWithId(courseId);
        }

        public List<Course> GetAll()
        {
            return _courseDal.GetAll();
        }
    }
}