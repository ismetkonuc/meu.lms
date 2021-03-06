using System;
using System.Collections.Generic;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.dto.Interfaces;
using meu.lms.entities.Enums;

namespace meu.lms.dto.DTOs.TaskDTOs
{
    public class TaskListDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public AssignmentListDto Assignment { get; set; }
    }
}