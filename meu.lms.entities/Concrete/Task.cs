using System;
using System.Collections.Generic;
using meu.lms.entities.Interface;
using meu.lms.entities.Enums;


namespace meu.lms.entities.Concrete
{
    public class Task : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(180);
        public Status Status { get; set; } = Status.Active;


        public List<Assignment> Assignments { get; set; }


        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}