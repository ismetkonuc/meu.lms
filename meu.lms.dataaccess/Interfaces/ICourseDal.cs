using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface ICourseDal : IGenericDal<Course>
    {
        List<Task> GetCourseTasks(int courseId);
        Course GetCourseWithId(int courseId);
        List<Course> GetAll();
    }
}