using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class CoursePeopleManager : ICoursePeopleService
    {

        private readonly ICoursePeopleDal _coursePeopleDal;

        public CoursePeopleManager(ICoursePeopleDal coursePeopleDal)
        {
            _coursePeopleDal = coursePeopleDal;
        }

        public void Save(CoursePeople table)
        {
            _coursePeopleDal.Save(table);
        }

        public void Delete(CoursePeople table)
        {
            _coursePeopleDal.Delete(table);
        }

        public void Update(CoursePeople table)
        {
            _coursePeopleDal.Update(table);
        }

        public CoursePeople GetTaskWithId(int id)
        {
            return _coursePeopleDal.GetTaskWithId(id);
        }

        public List<CoursePeople> GetAllTasks()
        {
            return _coursePeopleDal.GetAllTasks();
        }

        public List<Course> GetUserCourses(int userId)
        {
            return _coursePeopleDal.GetUserCourses(userId);
        }
    }
}