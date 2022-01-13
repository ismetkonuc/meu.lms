using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface IMessageDal : IGenericDal<Message>
    {
        public List<Message> GetAllMessages();
    }
}