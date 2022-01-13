using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(I => I.Id);

            builder.Property(I => I.Content).IsRequired().HasColumnType("ntext").HasColumnName("Content");
            builder.Property(I => I.MessageTo).IsRequired();
        }
    }
}