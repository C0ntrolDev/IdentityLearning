namespace IdentityLearning.Application.DTOs.Identity.User.DTOs
{
    public class LoginDto()
    {
        public string Password { get; init; } = null!;
        public string Email { get; init; } = null!;

        public string? DeviceName { get; set; }
        public string? Location { get; set; }
        public string? DeviceInfo { get; set; }
    }
}
