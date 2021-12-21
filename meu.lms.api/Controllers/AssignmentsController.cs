using AutoMapper;
using meu.lms.api.Enums;
using meu.lms.api.Models.AssignmentModels;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using meu.lms.api.CustomFilters;
using meu.lms.api.DevExtreme;
using meu.lms.api.Models;
using meu.lms.api.Models.CourseModels;
using meu.lms.api.Models.TaskModels;
using meu.lms.business.Interfaces;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.dto.DTOs.TaskDTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : BaseController
    {
        private readonly IAssignmentDal _assignmentDal;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly ICourseDal _courseDal;
        private readonly ITaskDal _taskDal;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;
        public AssignmentsController(IMapper mapper, IAssignmentDal assignmentDal, ICourseDal courseDal, ITaskDal taskDal, UserManager<AppUser> userManager, IAppUserService appUserService, IWebHostEnvironment appEnvironment) : base(assignmentDal)
        {
            _mapper = mapper;
            _assignmentDal = assignmentDal;
            _courseDal = courseDal;
            _taskDal = taskDal;
            _userManager = userManager;
            _appUserService = appUserService;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Consumes("multipart/form-data")]
        //public IActionResult Create([FromForm]AssignmentAddModel assignmentAddModel)
        public IActionResult Create([FromForm] AssignmentAddModel assignmentAddModel)
        {


            List<string> acceptedExtensions = new List<string>()
                {"image/jpeg", "image/jpg", "image/png", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/doc", "application/pdf", "application/zip", "application/vnd.rar"};

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            assignmentAddModel.AppUserId = appuserId;

            var uploadModel = UploadFile(assignmentAddModel.Attachment, acceptedExtensions, assignmentAddModel.AppUserId, assignmentAddModel.TaskId, false);

            if (uploadModel.UploadState == UploadState.Success)
            {
                assignmentAddModel.AttachmentPath = uploadModel.NewName;
                assignmentAddModel.IsSent = true;
                _assignmentDal.Save(_mapper.Map<Assignment>(assignmentAddModel));
            }

            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }


            return Created("", assignmentAddModel);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Update([FromForm] AssignmentUpdateModel assignmentUpdateModel)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var oldAssignment = _assignmentDal.GetSpesificAssignment(appuserId, assignmentUpdateModel.TaskId);

            assignmentUpdateModel.AppUserId = appuserId;
            assignmentUpdateModel.Id = oldAssignment.Id;


            List<string> acceptedExtensions = new List<string>()
                {"image/jpeg", "image/jpg", "image/png", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/doc", "application/pdf", "application/zip", "application/vnd.rar"};

            var uploadModel = UploadFile(assignmentUpdateModel.Attachment, acceptedExtensions, assignmentUpdateModel.AppUserId, assignmentUpdateModel.TaskId, true);


            if (uploadModel.UploadState == UploadState.Success)
            {
                assignmentUpdateModel.AttachmentPath = uploadModel.NewName;
                assignmentUpdateModel.IsSent = true;

                string webRootPath = _appEnvironment.WebRootPath + "\\assignments\\";
                var fullPath = webRootPath + oldAssignment.AttachmentPath;

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }

            else if (uploadModel.UploadState == UploadState.NotExist)
            {

                assignmentUpdateModel.AttachmentPath = null;
                assignmentUpdateModel.IsSent = false;
                _assignmentDal.Update(_mapper.Map<Assignment>(assignmentUpdateModel));

                return Ok(uploadModel.WarningMessage);
            }

            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }


            _assignmentDal.Update(_mapper.Map<Assignment>(assignmentUpdateModel));

            return NoContent();

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("sendedAssignments")]
        public IActionResult GetSendedAssignments(int courseId)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var currentCourseTaskIds = _taskDal.GetTasksWithCourseId(courseId).Select(I => I.Id);


            List<Assignment> assignments = new List<Assignment>();

            foreach (var taskId in currentCourseTaskIds)
            {

                try
                {
                    var assignment = _assignmentDal.GetAllAssignments().Where(I => I.TaskId == taskId && I.AppUserId == appuserId)?.First();

                    assignments.Add(assignment);
                }
                catch (Exception e)
                {
                    assignments.Add(new Assignment() { IsSent = false, AppUserId = appuserId, TaskId = taskId });
                }

            }

            //_assignmentDal.GetSpesificUserAssignments(appuserId).Where(I=>I.)

            return Ok(assignments);

        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("allAssignments")]
        [TypeFilter(typeof(ValidInstructorRole))]
        //public IActionResult GetAllAssignments(DataSourceLoadOptions loadOptions)
        public IActionResult GetAllAssignments()
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var userCourses = _appUserService.GetUserCourses(appuserId);

            List<CourseListModel> courseListModels = new List<CourseListModel>();
            List<TaskListModel> taskListModels = new List<TaskListModel>();

            foreach (var userCourse in userCourses)
            {
                var courseTasks = _taskDal.GetTasksWithCourseId(userCourse.Id);

                foreach (var courseTask in courseTasks)
                {
                    var assignments = _assignmentDal.GetAssignmentsWithTaskId(courseTask.Id);

                    var assignmentsListDto = _mapper.Map<List<AssignmentListDto>>(assignments);

                    taskListModels.Add(new TaskListModel()
                    {
                        CourseId = userCourse.Id,
                        CourseName = userCourse.Name,
                        CreationDate = courseTask.CreationDate,
                        Detail = courseTask.Detail,
                        ExpirationDate = courseTask.ExpirationDate,
                        Id = courseTask.Id,
                        Status = courseTask.Status,
                        Title = courseTask.Title,
                        Assignments = assignmentsListDto
                    });
                }

                courseListModels.Add(new CourseListModel()
                {
                    Code = userCourse.Code,
                    Id = userCourse.Id,
                    Name = userCourse.Name,
                    Tasks = taskListModels
                });

                taskListModels = new List<TaskListModel>();
            }

            //loadOptions.PrimaryKey = new[] { "id" };
            //loadOptions.PaginateViaPrimaryKey = true;
            //return Json(DataSourceLoader.Load(_courseDal.GetAll().AsEnumerable(), loadOptions));


            return Ok(courseListModels);
        }

        [HttpGet("assignScore/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [TypeFilter(typeof(ValidInstructorRole))]
        public async Task<IActionResult> AssignScore(int id) // and need taskId
        {
            var assignments = new List<Assignment>();

            assignments = _assignmentDal.GetAllAssignments().Where(I=>I.TaskId == id).ToList();

            List<AssignmentViewModel> assignmentViewModels = new List<AssignmentViewModel>();

            
            foreach (var assignment in assignments)
            {
                var user = await _userManager.FindByIdAsync(assignment.AppUserId.ToString());

                assignmentViewModels.Add(new AssignmentViewModel()
                {
                    FilePath = assignment.AttachmentPath,
                    Id = assignment.Id,
                    Score = assignment.Score,
                    UserFullName = $"{user.Name} {user.Surname}",
                    UserId = user.Id
                });
            }

            //var assignmentListDto = _mapper.Map<AssignmentListDto>(assignments);
            DataSourceLoadOptions loadOptions = new DataSourceLoadOptions();
            loadOptions.PrimaryKey = new[] { "id" };
            loadOptions.PaginateViaPrimaryKey = true;
            return Json(DataSourceLoader.Load(assignmentViewModels.AsEnumerable(), loadOptions));

        }

        [HttpPut("update")]
        public IActionResult UpdateScore([FromForm]AssignmentUpdateScoreModel updateScoreModel)
        {

            var result = JsonConvert.DeserializeObject<IDictionary<string, int>>(updateScoreModel.Values)["score"];

            var assignment = _assignmentDal.GetAllAssignments().Where(I => I.Id == updateScoreModel.Key).Single();

            assignment.Score = result;

            _assignmentDal.Update(assignment);


            return Ok();
            
        }
    }
}
