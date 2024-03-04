using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Queries.GetUserSessions
{
    public class GetUserSessionsQuery(long userId) : RequestWithUserId<Result<IEnumerable<SessionDto>>>(userId);
}
