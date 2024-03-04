using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.User.Commands.UpdateProfile
{
    public class UpdateProfileCommand(long userId, UpdateProfileDto updateProfileDto) : RequestWithUserId<Result<object>>(userId)
    {
        public UpdateProfileDto UpdateProfileDto { get; set; } = updateProfileDto;
    }
}
