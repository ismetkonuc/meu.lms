using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface IMessageService : IGenericService<Message>
    {
        public List<Message> GetAllMessages();

    }
}