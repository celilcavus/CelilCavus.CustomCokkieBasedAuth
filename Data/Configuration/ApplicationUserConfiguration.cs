using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CelilCavus.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.password).HasMaxLength(20).IsRequired();
            builder.Property(x => x.userName).HasMaxLength(50).IsRequired();

            builder.HasData(new ApplicationUser
            {
                Id = 1,
                userName = "celil",
                password = "1",
            });
        }
    }

    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Definition).HasMaxLength(50).IsRequired();

            builder.HasData(new ApplicationRole
            {
                Id = 1,
                Definition = "Admin"
            });
        }
    }

    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.ApplicationRole).WithMany(x => x.ApplicationUserRoles).HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.ApplicationUserRoles).HasForeignKey(x => x.UserId);


            builder.HasData(new ApplicationUserRole
            {
                RoleId = 1,
                UserId = 1
            });
        }
    }
}