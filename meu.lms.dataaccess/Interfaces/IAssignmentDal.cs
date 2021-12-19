using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface IAssignmentDal : IGenericDal<Assignment>
    {
        List<Assignment> GetAssignmentsWithTaskId(int taskId);
        List<Assignment> GetSpesificUserAssignments(int appUserId);
        List<Assignment> GetAllAssignments();
        Assignment GetSpesificAssignment(int appUserId, int taskId);
    }
}