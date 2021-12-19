using System;
using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class AssignmentManager : IAssignmentService
    {

        private readonly IAssignmentDal _assignmentDal;

        public AssignmentManager(IAssignmentDal assignmentDal)
        {
            _assignmentDal = assignmentDal;
        }

        public void Save(Assignment table)
        {
            _assignmentDal.Save(table);
        }

        public void Delete(Assignment table)
        {
            _assignmentDal.Delete(table);
        }

        public void Update(Assignment table)
        {
            _assignmentDal.Update(table);
        }

        public Assignment GetTaskWithId(int id)
        {
            return _assignmentDal.GetTaskWithId(id);
        }

        public List<Assignment> GetAllTasks()
        {
            return _assignmentDal.GetAllTasks();
        }

        public List<Assignment> GetAssignmentsWithTaskId(int taskId)
        {
            return _assignmentDal.GetAssignmentsWithTaskId(taskId);
        }

        public List<Assignment> GetSpesificUserAssignments(int appUserId)
        {
            return _assignmentDal.GetSpesificUserAssignments(appUserId);
        }

        public Assignment GetSpesificAssignment(int appUserId, int taskId)
        {
            return _assignmentDal.GetSpesificAssignment(appUserId, taskId);
        }

        public List<Assignment> GetAllAssignments()
        {
            return _assignmentDal.GetAllAssignments();
        }
    }
}