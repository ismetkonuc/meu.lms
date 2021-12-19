using System.Collections.Generic;
using meu.lms.entities.Concrete;

namespace meu.lms.dataaccess.Interfaces
{
    public interface ITaskDal : IGenericDal<Task>
    {
        List<Task> GetActiveTasks();
        List<Task> GetTasksWithCourseId(int courseId);
        Task GetTaskWithId(int taskId);

    }
}