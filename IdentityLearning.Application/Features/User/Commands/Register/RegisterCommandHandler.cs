using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Application.Options;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityLearning.Application.Features.User.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, IEmailSender emailSender)
        {
            _userRepository = userRepository;
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

            var isEmailFree = await _userRepository.IsEmailFree(request.RegisterDto.Email);
            if (isEmailFree == false)
            {
                return Result<string>.NotSuccessfull("Email not free", TotalErrorCode.Conflict);
            }

            var user = _mapper.Map<ApplicationUser>(request.RegisterDto);
            await _userRepository.CreateUser(user, request.RegisterDto.Password);

            var confirmationCode = await _userRepository.GenerateEmailConfirmationCode(user);
            
            _emailSender.SendEmailConfirmationMessage(user.Email!, confirmationCode, user.Id);

            return Result<string>.Successfull($"Email confirmation message was sent to email: {request.RegisterDto.Email}");
        }
    }
}
            