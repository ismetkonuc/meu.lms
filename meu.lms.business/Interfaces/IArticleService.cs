using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface IArticleService : IGenericService<Article>
    {
        List<Article> GetAll();

    }
}