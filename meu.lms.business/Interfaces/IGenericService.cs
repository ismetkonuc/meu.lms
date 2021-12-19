using System.Collections.Generic;
using meu.lms.entities.Interface;

namespace meu.lms.business.Interfaces
{
    public interface IGenericService<Table> where Table : class, ITable, new()
    {
        void Save(Table table);
        void Delete(Table table);
        void Update(Table table);
        Table GetTaskWithId(int id);
        List<Table> GetAllTasks();
    }
}