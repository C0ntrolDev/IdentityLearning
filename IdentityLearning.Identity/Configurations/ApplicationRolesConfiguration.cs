using IdentityLearning.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityLearning.Identity.Configurations;

public class ApplicationRolesConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new ApplicationRole()
            {
                Id = 2,
                Name = "Moderator",
                NormalizedName = "MODERATOR",
            },
            new ApplicationRole()
            {
                Id = 3,
                Name = "User",
                NormalizedName = "USER"
            });
    }
}