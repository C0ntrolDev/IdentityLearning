using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.User.Queries.GetProfile
{
    public class GetProfileCommand(long userId) : RequestWithUserId<Result<ProfileDto>>(userId);
}
