using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Application.DTOs.Identity.User.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Application.Features.User.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<object>>
    {
        private readonly IApplicationUserRepository _userRepository;

        public ConfirmEmailCommandHandler(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<object>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new ConfirmEmailDtoValidator();
            var validateResult = await validator.ValidateAsync(request.ConfirmEmailDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            if (user.EmailConfirmed)
            {
                return Result<object>.NotSuccessfull($"User email already confirmed", TotalErrorCode.Gone);
            }

            var confirmationResult = await _userRepository.ConfirmEmail(user, request.ConfirmEmailDto.ConfirmationCode);
            return confirmationResult.WithTotalErrorCode(TotalErrorCode.Forbidden);
        }
    }
}
