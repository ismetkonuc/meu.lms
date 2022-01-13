using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfMessageRepository: EfGenericRepository<Message>, IMessageDal
    {
        public List<Message> GetAllMessages()
        {
            using var db = new LmsDbContext();


            return db.Messages.ToList();
        }
    }
}