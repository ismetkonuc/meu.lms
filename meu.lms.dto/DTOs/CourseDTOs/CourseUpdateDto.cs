using meu.lms.dto.Interfaces;

namespace meu.lms.dto.DTOs.CourseDTOs
{
    public class CourseUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}