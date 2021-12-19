using meu.lms.entities.Interface;

namespace meu.lms.entities.Concrete
{
    public class CoursePeople : ITable
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }


        public int PersonId { get; set; }
        public AppUser AppUser { get; set; }

    }
}