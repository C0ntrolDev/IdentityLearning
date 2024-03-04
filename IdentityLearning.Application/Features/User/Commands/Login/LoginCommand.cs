using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.Login
{
    public record LoginCommand(LoginDto LoginDto) : IRequest<Result<RefreshAndAccessTokenDto>>;
}
