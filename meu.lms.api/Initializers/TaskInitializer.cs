using System;
using System.Collections.Generic;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using meu.lms.business.Interfaces;
using meu.lms.entities.Enums;

namespace meu.lms.api.Initializers
{
    public static class TaskInitializer
    {

        public static async Task SeedData(ITaskService _taskService)
        {

            if (!_taskService.GetAllTasks().Any())
            {


                List<entities.Concrete.Task> tasks = new List<entities.Concrete.Task>()
                {
                    new entities.Concrete.Task()
                    {
                        CourseId = 1,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(22),
                        Status = Status.Active,
                        Title = "At risus viverra adipiscing",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 3,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(5),
                        Status = Status.Active,
                        Title = "Porttitor massa id neque",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 2,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(10),
                        Status = Status.Active,
                        Title = "Massa massa ultricies",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 4,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Status = Status.Active,
                        Title = "Et sollicitudin ac orci",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 4,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Status = Status.Active,
                        Title = "Ullamcorper a lacus vestibulum",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 2,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Status = Status.Active,
                        Title = "Felis eget nunc lobortis",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 5,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Status = Status.Active,
                        Title = "Sapien pellentesque habitant",
                    },
                    new entities.Concrete.Task()
                    {
                        CourseId = 1,
                        CreationDate = DateTime.Now,
                        Detail =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lobortis feugiat vivamus at augue eget. At risus viverra adipiscing at in tellus. Massa massa ultricies mi quis hendrerit dolor magna eget. Eget nulla facilisi etiam dignissim diam quis enim.",
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Status = Status.Active,
                        Title = "Nec sagittis aliquam",
                    },
                };

                foreach (var task in tasks)
                {
                    _taskService.Save(task);
                }

            }
        }

    }
}