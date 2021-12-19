using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using meu.lms.entities.Interface;

namespace meu.lms.entities.Concrete
{
    public class Assignment : ITable
    {
        public int Id { get; set; }
        public string AttachmentPath { get; set; } = "/";
        public int Score { get; set; }
        public bool IsSent { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }



    }
}