﻿using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Account.Validators;
using IdentityLearning.Application.DTOs.Identity.Common.DTOs;
using IdentityLearning.Application.Models;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<RefreshAndAccessTokenDto>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mappper;

        public LoginCommandHandler(IAccountUserRepository accountUserUserRepository, ISessionRepository sessionRepository, IMapper mappper)
        {
            _accountUserUserRepository = accountUserUserRepository;
            _sessionRepository = sessionRepository;
            _mappper = mappper;
        }

        public async Task<Result<RefreshAndAccessTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginDtoValidator();
            var validateResult = await validator.ValidateAsync(request.LoginDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult<RefreshAndAccessTokenDto>();
            }

            var user = await _accountUserUserRepository.GetUserByCredentials(request.LoginDto.Email, request.LoginDto.Password);
            if (user == null)
            {
                return Result<RefreshAndAccessTokenDto>.NotSuccessfull($"User with email: {request.LoginDto.Email} and password: {request.LoginDto.Password} not found", TotalErrorCode.NotFound);
            }

            if (user.EmailConfirmed == false)
            {
                return Result<RefreshAndAccessTokenDto>.NotSuccessfull($"User's email has not been verified", TotalErrorCode.Forbidden);
            }

            var sessionInfo = _mappper.Map<ApplicationUserSessionInfo>(request.LoginDto);
            var createUserSessionResult = await _sessionRepository.CreateUserSession(user, sessionInfo);

            return Result<RefreshAndAccessTokenDto>.Successfull(_mappper.Map<RefreshAndAccessTokenDto>(createUserSessionResult));
        }
    }
}
