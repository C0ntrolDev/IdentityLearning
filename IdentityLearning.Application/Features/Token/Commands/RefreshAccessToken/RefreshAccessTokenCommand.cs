using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Token.Commands.RefreshAccessToken
{
    public record RefreshAccessTokenCommand(RefreshAccessTokenDto RefreshAccessTokenDto)
        : IRequest<Result<RefreshAndAccessTokenDto>>;
}
