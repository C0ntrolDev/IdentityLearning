using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly IEmailSender _emailSender;

        public DeleteUserCommandHandler(IAccountUserRepository accountUserUserRepository, IEmailSender emailSender)
        {
            _accountUserUserRepository = accountUserUserRepository;
            _emailSender = emailSender;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var code = await _accountUserUserRepository.GenerateDeleteUserCode(user);
            _emailSender.SendDeleteAccountConfirmationMessage(user.Email!, code, request.UserId);

            return Result<object>.Successfull(null!);
        }
    }
}
