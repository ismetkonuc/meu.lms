using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts
{
    public class LmsDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-0GDA6G3; Database = lms; User Id = sa; Password = 1;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            //builder.ApplyConfiguration(new AppUserRoleConfiguration());
            //builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new CoursePeopleConfiguration());
            builder.ApplyConfiguration(new AssignmentConfiguration());
            builder.ApplyConfiguration(new ArticleConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }
        //public DbSet<AppUserRole> AppUserRoles { get; set; }
        //public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<CoursePeople> CoursePeoples { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}