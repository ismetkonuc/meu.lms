using System.Collections.Generic;
using System.Security.AccessControl;

namespace meu.lms.api.Models.MessageModels
{
    public class MessageListModel
    {
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}