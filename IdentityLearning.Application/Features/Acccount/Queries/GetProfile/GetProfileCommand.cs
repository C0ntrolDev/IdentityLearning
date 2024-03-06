using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.Features.Common;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Features.Acccount.Queries.GetProfile
{
    public class GetProfileCommand(long userId) : RequestWithUserId<Result<ProfileDto>>(userId);
}
