using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand(long userId, ConfirmEmailDto confirmEmailDto) : RequestWithUserId<Result<object>>(userId)
    {
        public ConfirmEmailDto ConfirmEmailDto { get; } = confirmEmailDto;
    }

}
