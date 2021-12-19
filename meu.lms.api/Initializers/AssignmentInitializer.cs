using System.Collections.Generic;
using System.Linq;
using meu.lms.business.Interfaces;
using meu.lms.entities.Concrete;
using Task = System.Threading.Tasks.Task;

namespace meu.lms.api.Initializers
{
    public static class AssignmentInitializer
    {
        public static async Task SeedData(IAssignmentService _assignmentService)
        {
            if (!_assignmentService.GetAllAssignments().Any())
            {

                List<Assignment> assignments = new List<Assignment>()
                {
                    new Assignment()
                    {
                        AppUserId = 2,
                        IsSent = true,
                        Score = 10,
                        TaskId = 2,
                        AttachmentPath = "."
                    },
                    new Assignment()
                    {
                        AppUserId = 2,
                        IsSent = true,
                        Score = 20,
                        TaskId = 5,
                        AttachmentPath = "."
                    },
                    new Assignment()
                    {
                        AppUserId = 2,
                        IsSent = true,
                        Score = 50,
                        TaskId = 3,
                        AttachmentPath = "."
                    },
                    new Assignment()
                    {
                        AppUserId = 2,
                        IsSent = true,
                        Score = 100,
                        TaskId = 4,
                        AttachmentPath = "."
                    },
                    new Assignment()
                    {
                        AppUserId = 2,
                        IsSent = true,
                        Score = 90,
                        TaskId = 1,
                        AttachmentPath = "."
                    },
                };

                foreach (var assignment in assignments)
                {
                    _assignmentService.Save(assignment);
                }

            }
        }
    }
}