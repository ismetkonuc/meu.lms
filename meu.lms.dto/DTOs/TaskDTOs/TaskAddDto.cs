using System;
using meu.lms.dto.Interfaces;
using meu.lms.entities.Enums;

namespace meu.lms.dto.DTOs.TaskDTOs
{
    public class TaskAddDto : IDto
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }

        public int CourseId { get; set; }

    }
}