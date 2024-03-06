using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Account.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.UpdatePassword
{
    internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<object>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;

        public UpdatePasswordCommandHandler(IAccountUserRepository accountUserUserRepository)
        {
            _accountUserUserRepository = accountUserUserRepository;
        }

        public async Task<Result<object>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePasswordDtoValidator();
            var validateResult = await validator.ValidateAsync(request.UpdatePasswordDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            return await _accountUserUserRepository.UpdatePassword(user, request.UpdatePasswordDto.NewPassword);
        }
    }
}
