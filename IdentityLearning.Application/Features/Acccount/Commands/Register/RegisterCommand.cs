using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.Register
{
    public record RegisterCommand(RegisterDto RegisterDto) : IRequest<Result<string>>;


}
