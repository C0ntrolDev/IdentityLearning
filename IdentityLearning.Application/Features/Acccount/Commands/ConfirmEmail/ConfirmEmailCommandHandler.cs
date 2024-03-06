using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Account.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<object>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;

        public ConfirmEmailCommandHandler(IAccountUserRepository accountUserUserRepository)
        {
            _accountUserUserRepository = accountUserUserRepository;
        }

        public async Task<Result<object>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new ConfirmEmailDtoValidator();
            var validateResult = await validator.ValidateAsync(request.ConfirmEmailDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            if (user.EmailConfirmed)
            {
                return Result<object>.NotSuccessfull($"User email already confirmed", TotalErrorCode.Gone);
            }

            var confirmationResult = await _accountUserUserRepository.ConfirmEmail(user, request.ConfirmEmailDto.ConfirmationCode);
            return confirmationResult.WithTotalErrorCode(TotalErrorCode.Forbidden);
        }
    }
}
