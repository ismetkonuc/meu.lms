using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using meu.lms.business.Interfaces;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.dto.DTOs.CourseDTOs;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ITaskService _taskService;
        private readonly IAssignmentService _assignmentService;
        private readonly IMapper _mapper;
        public CoursesController(ICourseService courseService, IMapper mapper, ITaskService taskService, IAssignmentService assignmentService)
        {
            _courseService = courseService;
            _mapper = mapper;
            _taskService = taskService;
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<CourseListDto>>(_courseService.GetAll()));
        }

        
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            return Ok(_mapper.Map<CourseListDto>(_courseService.GetCourseWithId(id)));
        }

        [HttpPost]
        public IActionResult Create(CourseAddDto courseAddDto)
        {

            _courseService.Save(_mapper.Map<Course>(courseAddDto));

            return Created("", courseAddDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CourseUpdateDto courseUpdateDto)
        {

            if (id != courseUpdateDto.Id)
            {
                return BadRequest("Invalid Id");
            }

            _courseService.Update(_mapper.Map<Course>(courseUpdateDto));

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseService.Delete(new Course(){Id = id});

            return NoContent();
        }
    }
}
