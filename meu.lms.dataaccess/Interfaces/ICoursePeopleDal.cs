using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface ICoursePeopleDal : IGenericDal<CoursePeople>
    {
        List<Course> GetUserCourses(int userId);
    }
}