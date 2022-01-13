using System;

namespace meu.lms.api.Models.MessageModels
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public string Type { get; set; }
    }
}