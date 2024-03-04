using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Domain.Entities.User;

public class ApplicationUserSession
{
    public long Id { get; set; }

    public long ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public string? DeviceName { get; set; }
    public string? Location { get; set; }
    public string? DeviceInfo { get; set; }
    public DateTime? CreationTime { get; set; }

    public Guid? RefreshTokenId { get; set; }
    public TokenInfo? AccessToken { get; set; }
}