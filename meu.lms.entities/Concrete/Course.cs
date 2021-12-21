using System.Collections.Generic;
using meu.lms.entities.Interface;

namespace meu.lms.entities.Concrete
{
    public class Course : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


        public List<CoursePeople> People { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Article> Articles { get; set; }

    }
}