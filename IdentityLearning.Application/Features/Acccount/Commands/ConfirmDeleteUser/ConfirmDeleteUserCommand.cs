using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.ConfirmDeleteUser
{
    public record ConfirmDeleteUserCommand(string Code, long UserId) : IRequest<Result<object>>
    {
    }
}
