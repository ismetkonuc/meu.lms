using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface ICourseService : IGenericService<Course>
    {
        List<Task> GetCourseTasks(int courseId);
        Course GetCourseWithId(int courseId);
        List<Course> GetAll();
    }
}