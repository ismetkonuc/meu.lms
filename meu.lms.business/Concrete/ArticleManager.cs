using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace meu.lms.business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public void Save(Article table)
        {
            _articleDal.Save(table);
        }

        public void Delete(Article table)
        {
            _articleDal.Delete(table);
        }

        public void Update(Article table)
        {
            _articleDal.Update(table);
        }

        public Article GetTaskWithId(int id)
        {
            return _articleDal.GetTaskWithId(id);
        }

        public List<Article> GetAllTasks()
        {
            return _articleDal.GetAllTasks();
        }

        public List<Article> GetAll()
        {
            return _articleDal.GetAll();
        }
    }
}