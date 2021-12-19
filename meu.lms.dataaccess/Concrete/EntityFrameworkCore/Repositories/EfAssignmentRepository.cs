using System.Collections.Generic;
using System.Linq;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAssignmentRepository : EfGenericRepository<Assignment>, IAssignmentDal
    {
        public List<Assignment> GetAssignmentsWithTaskId(int taskId)
        {
            using var db = new LmsDbContext();

            return db.Assignments.Where(I => I.Id == taskId).ToList();

            //return db.Tasks.Single(I => I.Id == taskId).Assignments;

        }


        public List<Assignment> GetSpesificUserAssignments(int appUserId)
        {
            using var db = new LmsDbContext();

            var appUser = db.Users.First(I => I.Id == appUserId);

            return appUser.Assignments;

        }

        public Assignment GetSpesificAssignment(int appUserId, int taskId)
        {
            using var db = new LmsDbContext();



            return db.Assignments?.Where(I => I.TaskId == taskId && I.AppUserId == appUserId).First();

        }

        public List<Assignment> GetAllAssignments()
        {
            using var db = new LmsDbContext();

            return db.Assignments.ToList();
        }
    }
}