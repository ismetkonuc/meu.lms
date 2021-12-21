using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {


        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.Text).HasColumnType("ntext");
        }
    }
}