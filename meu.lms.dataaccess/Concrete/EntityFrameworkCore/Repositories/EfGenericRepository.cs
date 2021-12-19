using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Interface;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<Table> : IGenericDal<Table> where Table : class, ITable, new()
    {
        public void Save(Table table)
        {
            using var db = new LmsDbContext();

            db.Set<Table>().Add(table);
            db.SaveChanges();
        }

        public void Delete(Table table)
        {
            using var db = new LmsDbContext();
            db.Set<Table>().Remove(table);
            db.SaveChanges();
        }

        public void Update(Table table)
        {
            using var db = new LmsDbContext();
            db.Set<Table>().Update(table);
            db.SaveChanges();
        }

        public Table GetTaskWithId(int id)
        {
            using var db = new LmsDbContext();

            return db.Set<Table>().Find(id);
        }

        public List<Table> GetAllTasks()
        {
            using var db = new LmsDbContext();

            return db.Set<Table>().ToList();
        }
    }
}