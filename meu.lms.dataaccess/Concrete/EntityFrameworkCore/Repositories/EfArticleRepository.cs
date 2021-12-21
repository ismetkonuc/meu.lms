using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfArticleRepository : EfGenericRepository<Article>, IArticleDal
    {
        public List<Article> GetAll()
        {
            using var db = new LmsDbContext();

            return db.Articles.ToList();
        }
    }
}