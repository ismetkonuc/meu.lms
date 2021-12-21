using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> GetAll();
    }
}