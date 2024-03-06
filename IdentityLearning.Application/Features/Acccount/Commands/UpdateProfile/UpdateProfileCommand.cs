using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.Acccount.Commands.UpdateProfile
{
    public class UpdateProfileCommand(long userId, UpdateProfileDto updateProfileDto) : RequestWithUserId<Result<object>>(userId)
    {
        public UpdateProfileDto UpdateProfileDto { get; set; } = updateProfileDto;
    }
}
