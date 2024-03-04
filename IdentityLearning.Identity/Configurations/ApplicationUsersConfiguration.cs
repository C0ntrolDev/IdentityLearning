using IdentityLearning.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityLearning.Identity.Configurations;

public class ApplicationUsersConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(
            new ApplicationUser()
            {
                Id = 1,
                Name = "ControlDev", 
                UserName = "erdcontroldev@gmail.com",
                NormalizedUserName = "ERDCONTROLDEV@GMAIL.COM",
                DateOfBirth = new DateTime(2008, 02, 11),
                Email = "erdcontroldev@gmail.com",
                NormalizedEmail = "ERDCONTROLDEV@GMAIL.COM",
            });
    }
}