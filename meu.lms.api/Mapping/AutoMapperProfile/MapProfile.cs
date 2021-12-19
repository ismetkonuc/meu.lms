using AutoMapper;
using meu.lms.api.Models.AssignmentModels;
using meu.lms.dto.DTOs.AssignmentDTOs;
using meu.lms.dto.DTOs.CourseDTOs;
using meu.lms.dto.DTOs.TaskDTOs;
using meu.lms.entities.Concrete;

namespace meu.lms.api.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //Mapping of Courses
            CreateMap<CourseAddDto, Course>();
            CreateMap<Course, CourseAddDto>();

            CreateMap<CourseListDto, Course>();
            CreateMap<Course, CourseListDto>();

            CreateMap<CourseUpdateDto, Course>();
            CreateMap<Course, CourseUpdateDto>();


            //Mapping of Tasks

            CreateMap<TaskAddDto, Task>();
            CreateMap<Task, TaskAddDto>();

            CreateMap<TaskListDto, Task>();
            CreateMap<Task, TaskListDto>();

            CreateMap<TaskUpdateDto, Task>();
            CreateMap<Task, TaskUpdateDto>();

            //Mapping of Assignments

            CreateMap<AssignmentAddModel, Assignment>();
            CreateMap<Assignment, AssignmentAddModel>();

            CreateMap<AssignmentListDto, Assignment>();
            CreateMap<Assignment, AssignmentListDto>();

            CreateMap<AssignmentUpdateModel, Assignment>();
            CreateMap<Assignment, AssignmentUpdateModel>();

        }
    }
}