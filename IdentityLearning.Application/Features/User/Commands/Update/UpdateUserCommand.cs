using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.Features.User.Commands.Update
{
    public record UpdateUserCommand(long UserId, UserDto User) : IRequest<Result<object>>;
    
}
