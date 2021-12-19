using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace meu.lms.api.Models.AssignmentModels
{
    public class AssignmentUpdateModel
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int TaskId { get; set; }
        public IFormFile Attachment { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsSent { get; set; }
    }
}