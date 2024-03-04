using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityLearning.Identity.Configurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.HasData(
            new IdentityUserRole<long>()
            {
                UserId = 1,
                RoleId = 1
            });
    }
}