using meu.lms.entities.Concrete;

namespace meu.lms.api.Models.MessageModels
{
    public class SendMessageModel
    {
        public string Content { get; set; }
        public int MessageTo { get; set; } //Reciver User ID

    }
}