using System;
using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Code).HasMaxLength(20).IsRequired();

            builder.HasMany(I => I.Tasks).WithOne(I => I.Course).HasForeignKey(I => I.CourseId);
            builder.HasMany(I => I.People).WithOne(I => I.Course).HasForeignKey(I => I.PersonId);
            builder.HasMany(I => I.Articles).WithOne(I => I.Course).HasForeignKey(I => I.CourseId);
        }
    }
}