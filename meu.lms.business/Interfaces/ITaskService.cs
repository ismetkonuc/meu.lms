using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Interfaces
{
    public interface ITaskService : IGenericService<Task>
    {
        List<Task> GetActiveTasks();
        List<Task> GetTasksWithCourseId(int courseId);
        Task GetTaskWithId(int taskId);
    }
}