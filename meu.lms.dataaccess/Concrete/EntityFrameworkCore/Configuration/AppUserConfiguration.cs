using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            
            builder.Property(I => I.Name).HasMaxLength(100);
            builder.Property(I => I.Surname).HasMaxLength(100);

            builder.HasMany(I => I.Courses).WithOne(I => I.AppUser).HasForeignKey(I => I.CourseId);
            builder.HasMany(I => I.Assignments).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId);
            builder.HasMany(I => I.Articles).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId);
        }
    }


}