using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Description { get; set; }

        public List<ApplicationUserSession>? Sessions { get; set; }
    }
}
