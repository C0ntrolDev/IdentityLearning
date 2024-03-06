using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.Register
{
    public record RegisterCommand(RegisterDto RegisterDto) : IRequest<Result<string>>;


}
