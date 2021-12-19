using System.Collections.Generic;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.business.Concrete
{
    public class TaskManager : ITaskService
    {

        private readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
        }

        public void Save(Task table)
        {
            _taskDal.Save(table);
        }

        public void Delete(Task table)
        {
            _taskDal.Delete(table);
        }

        public void Update(Task table)
        {
            _taskDal.Update(table);
        }

        Task ITaskService.GetTaskWithId(int taskId)
        {
            return _taskDal.GetTaskWithId(taskId);
        }

        public List<Task> GetActiveTasks()
        {
            return _taskDal.GetActiveTasks();
        }

        public List<Task> GetTasksWithCourseId(int courseId)
        {
            return _taskDal.GetTasksWithCourseId(courseId);
        }

        Task IGenericService<Task>.GetTaskWithId(int id)
        {
            return _taskDal.GetTaskWithId(id);
        }

        public List<Task> GetAllTasks()
        {
            return _taskDal.GetAllTasks();
        }
    }
}