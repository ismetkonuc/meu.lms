using System;
using meu.lms.entities.Interface;

namespace meu.lms.entities.Concrete
{
    public class Message : ITable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int MessageTo { get; set; } //Reciver User ID
        public DateTime SendedDate { get; set; } = DateTime.Now;

        public int AppUserId { get; set; } //Sender User ID
        public AppUser AppUser { get; set; }

    }
}