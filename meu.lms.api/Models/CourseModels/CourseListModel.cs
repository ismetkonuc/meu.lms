using System.Collections.Generic;
using meu.lms.api.Models.TaskModels;
using meu.lms.dto.DTOs.TaskDTOs;

namespace meu.lms.api.Models.CourseModels
{
    public class CourseListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<TaskListModel> Tasks { get; set; }

    }
}