using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.Login
{
    public record LoginCommand(LoginDto LoginDto) : IRequest<Result<RefreshAndAccessTokenDto>>;
}
