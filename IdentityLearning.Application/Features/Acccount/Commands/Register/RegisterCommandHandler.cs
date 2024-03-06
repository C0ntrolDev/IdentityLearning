using AutoMapper;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Application.DTOs.Identity.Account.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IAccountUserRepository accountUserUserRepository, IMapper mapper, IEmailSender emailSender)
        {
            _accountUserUserRepository = accountUserUserRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RegisterDto, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToResult<string>();
            }

            var isEmailFree = await _accountUserUserRepository.IsEmailFree(request.RegisterDto.Email);
            if (isEmailFree == false)
            {
                return Result<string>.NotSuccessfull("Email not free", TotalErrorCode.Conflict);
            }

            var user = _mapper.Map<ApplicationUser>(request.RegisterDto);
            await _accountUserUserRepository.CreateUser(user, request.RegisterDto.Password);

            var confirmationCode = await _accountUserUserRepository.GenerateEmailConfirmationCode(user);
            
            _emailSender.SendEmailConfirmationMessage(user.Email!, confirmationCode, user.Id);

            return Result<string>.Successfull($"Email confirmation message was sent to email: {request.RegisterDto.Email}");
        }
    }
}
            