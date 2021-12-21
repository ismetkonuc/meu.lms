using System.Security.AccessControl;
using meu.lms.entities.Interface;

namespace meu.lms.entities.Concrete
{
    public class Article : ITable
    {
        public int Id { get; set; }
        public string Text { get; set; }


        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}