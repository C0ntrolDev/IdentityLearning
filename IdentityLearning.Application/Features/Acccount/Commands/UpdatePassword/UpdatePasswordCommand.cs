using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.Acccount.Commands.UpdatePassword
{
    public class UpdatePasswordCommand(long userId, UpdatePasswordDto updatePasswordDto) : RequestWithUserId<Result<object>>(userId)
    {
        public UpdatePasswordDto UpdatePasswordDto { get; } = updatePasswordDto;
    }
}
