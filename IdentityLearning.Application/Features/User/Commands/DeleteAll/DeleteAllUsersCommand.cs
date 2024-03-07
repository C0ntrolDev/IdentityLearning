using IdentityLearning.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.Features.User.Commands.DeleteAll
{
    public class DeleteAllUsersCommand : IRequest<Result<object>>;
}
