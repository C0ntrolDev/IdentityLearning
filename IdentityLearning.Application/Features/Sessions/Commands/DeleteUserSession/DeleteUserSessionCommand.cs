using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Commands.DeleteUserSession
{
    public record DeleteUserSessionCommand(DeleteUserSessionDto DeleteUserSessionDto) : IRequest<Result<object>>;
}
