using System;
using System.Collections.Generic;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.entities.Enums;

namespace meu.lms.api.Models.TaskModels
{
    public class TaskListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public List<AssignmentListDto> Assignments { get; set; }
    }
}