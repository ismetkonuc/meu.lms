using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using meu.lms.api.CustomFilters;
using meu.lms.business.Interfaces;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.dto.DTOs.TaskDTOs;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IAssignmentService _assignmentService;
        public TasksController(IMapper mapper, ITaskService taskService, IAssignmentService assignmentService)
        {
            _mapper = mapper;
            _taskService = taskService;
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<TaskListDto>>(_taskService.GetAllTasks()));
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourseTasks(int courseId)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var tasks = _mapper.Map<List<TaskListDto>>(_taskService.GetTasksWithCourseId(courseId));

            foreach (var task in tasks)
            {

                AssignmentListDto assignmentListDto = new AssignmentListDto()
                {
                    AppUserId = appuserId,
                    TaskId = task.Id
                };

                try
                {
                    var assignment = _assignmentService.GetAllAssignments().Where(I => I.TaskId == task.Id && I.AppUserId == appuserId)?.First();

                    task.Assignment = _mapper.Map<AssignmentListDto>(assignment);
                }
                catch (Exception e)
                {
                    task.Assignment = assignmentListDto;
                }
            }

            return Ok(tasks);
        }

        [HttpGet("{courseId}/{taskId}")]
        public IActionResult GetSingleTask(int courseId,int taskId)
        {
            var courseTasks = _taskService.GetTasksWithCourseId(courseId);

            return Ok(_mapper.Map<TaskListDto>(courseTasks.Where(I => I.Id == taskId).Single()));
        }

        [HttpGet("single/{taskId}")]
        public IActionResult GetSingleTask(int taskId)
        {
            var task = _taskService.GetTaskWithId(taskId);

            return Ok(_mapper.Map<TaskListDto>(task));
        }

        [HttpPost]
        [TypeFilter(typeof(ValidInstructorRole))]
        public IActionResult CreateTask([FromBody] TaskAddDto taskAddDto)
        {

            try
            {
                _taskService.Save(_mapper.Map<Task>(taskAddDto));
            }
            catch (Exception e)
            {
                return Problem(statusCode: 500);

            }


            return Created("", taskAddDto);
        }

        [HttpPut]
        [TypeFilter(typeof(ValidInstructorRole))]

        public IActionResult UpdateTask([FromBody] TaskUpdateDto taskUpdateDto)
        {

            //if (taskUpdateDto != taskUpdateDto.Id)
            //{
            //    return BadRequest("Geçersiz ID");
            //}

            var updatedTask = _taskService.GetTaskWithId(taskUpdateDto.Id);

            updatedTask.Id = taskUpdateDto.Id;
            updatedTask.CourseId = taskUpdateDto.CourseId;
            updatedTask.ExpirationDate = taskUpdateDto.ExpirationDate;
            updatedTask.Title = taskUpdateDto.Title;
            updatedTask.Detail = taskUpdateDto.Detail;
            updatedTask.Status = taskUpdateDto.Status;

            try
            {
                _taskService.Update(updatedTask);
            }
            catch (Exception e)
            {
                return Problem(statusCode: 500);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {

            try
            {
                _taskService.Delete(new Task() { Id = id });
            }
            catch (Exception e)
            {
                return Problem(statusCode: 500);
            }

            return NoContent();
        }

    }

}
