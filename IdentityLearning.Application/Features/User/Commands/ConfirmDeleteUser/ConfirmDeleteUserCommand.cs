using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.ConfirmDeleteUser
{
    public record ConfirmDeleteUserCommand(string Code, long UserId) : IRequest<Result<object>>
    {
    }
}
