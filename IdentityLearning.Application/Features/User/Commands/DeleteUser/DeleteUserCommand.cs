using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand(long userId) : RequestWithUserId<Result<object>>(userId);
}
