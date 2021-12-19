using meu.lms.entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meu.lms.dataaccess.Concrete.EntityFrameworkCore.Configuration
{
    //public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    //{
    //    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    //    {
    //        builder.HasKey(I => I.Id);
    //        builder.Property(I => I.Id).UseIdentityColumn();

    //        builder.HasIndex(I => new { I.AppUserId, I.AppRoleId }).IsUnique();
    //    }
    //}
    public class AppUserRoleConfiguration
    {

    }
}