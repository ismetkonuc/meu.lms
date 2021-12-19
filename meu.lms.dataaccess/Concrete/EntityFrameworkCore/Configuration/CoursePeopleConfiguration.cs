using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class CoursePeopleConfiguration : IEntityTypeConfiguration<CoursePeople>
    {
        public void Configure(EntityTypeBuilder<CoursePeople> builder)
        {
            builder.HasKey(k => new {k.CourseId, k.PersonId});
            //builder.Property(I => new {I.CourseId, I.PersonId}).UseIdentityColumn();

            builder.HasOne(I => I.AppUser).WithMany(I => I.Courses).HasForeignKey(I => I.PersonId);
            builder.HasOne(I => I.Course).WithMany(I => I.People).HasForeignKey(I => I.CourseId);
        }
    }
}