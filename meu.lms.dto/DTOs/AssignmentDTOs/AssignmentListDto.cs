using meu.lms.dto.Interfaces;

namespace meu.lms.dto.DTOs.AssignmentDTOs
{
    public class AssignmentListDto : IDto
    {
        public int Id { get; set; }
        public string AttachmentPath { get; set; } = "/";
        public int Score { get; set; } = 0;
        public bool IsSent { get; set; } = false;


        public int AppUserId { get; set; }
        public int TaskId { get; set; }

    }
}