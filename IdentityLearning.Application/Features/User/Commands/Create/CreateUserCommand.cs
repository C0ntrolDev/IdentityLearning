using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.Create
{
    public record CreateUserCommand(UserDto User) : IRequest<Result<object>>;
}
