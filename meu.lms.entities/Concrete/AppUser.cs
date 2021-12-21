using System.Collections.Generic;
using meu.lms.entities.Interface;
using Microsoft.AspNetCore.Identity;

namespace meu.lms.entities.Concrete
{
    public class AppUser : IdentityUser<int>, ITable
    {
        public string Name { get; set; }
        public string Surname { get; set; }


        public List<CoursePeople> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Article> Articles { get; set; }

    }
}