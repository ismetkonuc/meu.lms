using meu.lms.business.Concrete;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Repositories;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace meu.lms.business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAssignmentService, AssignmentManager>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<ITaskDal, EfTaskRepository>();
            services.AddScoped<ICourseDal, EfCourseRepository>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IAssignmentDal, EfAssignmentRepository>();


            //services.AddScoped(typeof(IGenericService<>), typeof(Generic))
        }

    }
}