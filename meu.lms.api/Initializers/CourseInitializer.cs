using System.Collections.Generic;
using System.Linq;
using meu.lms.business.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace meu.lms.api.Initializers
{
    public class CourseInitializer
    {
        public static async Task SeedData(ICourseService _courseService)
        {
            if (!_courseService.GetAll().Any())
            {
                List<Course> courses = new List<Course>()
                {
                    new Course()
                    {
                        Name = "Bitirme Ödevi - 1",
                        Code = "175001"
                    },
                    new Course()
                    {
                        Name = "Image Processing",
                        Code = "175002"
                    },
                    new Course()
                    {
                        Name = "Machine Learning",
                        Code = "175003"
                    },
                    new Course()
                    {
                        Name = "Psikolojiye Giriş",
                        Code = "175004"
                    },
                    new Course()
                    {
                        Name = "Digital Control Systems",
                        Code = "175005"
                    },
                };

                foreach (var course in courses)
                {
                    _courseService.Save(course);   
                }

            }
        }
    }
}