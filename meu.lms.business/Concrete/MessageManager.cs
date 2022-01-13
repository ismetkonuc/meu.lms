using System;
using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal ?? throw new ArgumentNullException(nameof(messageDal));
        }

        public void Save(Message table)
        {
            _messageDal.Save(table);
        }

        public void Delete(Message table)
        {
            _messageDal.Delete(table);
        }

        public void Update(Message table)
        {
            _messageDal.Save(table);
        }

        public Message GetTaskWithId(int id)
        {
            return _messageDal.GetTaskWithId(id);
        }

        public List<Message> GetAllTasks()
        {
            return _messageDal.GetAllTasks();
        }

        public List<Message> GetAllMessages()
        {
            return _messageDal.GetAllMessages();
        }
    }
}