using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Queries.Get
{
    public record GetUserQuery(long UserId) : IRequest<Result<GetUserResponseDto>>;
}
