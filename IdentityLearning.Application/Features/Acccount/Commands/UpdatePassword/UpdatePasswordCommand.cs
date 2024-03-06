using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.User.Commands.UpdatePassword
{
    public class UpdatePasswordCommand(long userId, UpdatePasswordDto updatePasswordDto) : RequestWithUserId<Result<object>>(userId)
    {
        public UpdatePasswordDto UpdatePasswordDto { get; } = updatePasswordDto;
    }
}
