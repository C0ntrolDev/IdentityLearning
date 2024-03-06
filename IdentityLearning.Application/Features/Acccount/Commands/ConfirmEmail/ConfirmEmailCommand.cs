using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.Acccount.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand(long userId, ConfirmEmailDto confirmEmailDto) : RequestWithUserId<Result<object>>(userId)
    {
        public ConfirmEmailDto ConfirmEmailDto { get; } = confirmEmailDto;
    }

}
