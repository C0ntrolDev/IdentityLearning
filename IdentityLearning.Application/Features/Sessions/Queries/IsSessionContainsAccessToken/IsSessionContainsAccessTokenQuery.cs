using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Queries.IsSessionContainsAccessToken
{
    public record IsSessionContainsAccessTokenQuery(IsSessionContainsAccessTokenDto IsSessionContainsAccessTokenDto) : IRequest<Result<bool>>;
}
