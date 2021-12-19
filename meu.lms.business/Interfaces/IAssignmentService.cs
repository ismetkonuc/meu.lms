using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface IAssignmentService : IGenericService<Assignment>
    {
        List<Assignment> GetAssignmentsWithTaskId(int taskId);
        List<Assignment> GetSpesificUserAssignments(int appUserId);
        Assignment GetSpesificAssignment(int appUserId, int taskId);
        List<Assignment> GetAllAssignments();

    }
}