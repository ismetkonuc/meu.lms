using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.ExpirationDate).IsRequired();
            builder.Property(I => I.Title).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Detail).HasColumnType("ntext");
            builder.Property(I => I.Detail).HasColumnName("Detail");
            builder.Property(I => I.CreationDate).IsRequired();

            builder.HasMany(I => I.Assignments).WithOne(I => I.Task).HasForeignKey(I => I.TaskId);
        }
    }
}