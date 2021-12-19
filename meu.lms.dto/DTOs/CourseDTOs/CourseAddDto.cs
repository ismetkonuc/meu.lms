using meu.lms.dto.Interfaces;

namespace meu.lms.dto.DTOs.CourseDTOs
{
    public class CourseAddDto : IDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}