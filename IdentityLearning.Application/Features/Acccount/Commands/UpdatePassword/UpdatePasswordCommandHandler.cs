﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.UpdatePassword
{
    internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<object>>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdatePasswordCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<object>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePasswordDtoValidator();
            var validateResult = await validator.ValidateAsync(request.UpdatePasswordDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = await _accountRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            return await _accountRepository.UpdatePassword(user, request.UpdatePasswordDto.NewPassword);
        }
    }
}