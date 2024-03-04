using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Token.Queries.IsUserClaimsInBlacklist
{
    public record IsUserClaimsInBlacklistQuery(IsUserClaimsInBlacklistDto IsUserClaimsInBlacklistDto) : IRequest<Result<bool>>;
}
