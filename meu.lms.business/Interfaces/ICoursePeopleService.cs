using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface ICoursePeopleService : IGenericService<CoursePeople>
    {
        List<Course> GetUserCourses(int userId);

    }
}