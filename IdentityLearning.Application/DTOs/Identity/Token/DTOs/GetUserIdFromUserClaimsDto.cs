using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.Token.DTOs
{
    public class GetUserIdFromUserClaimsDto
    {
        public IEnumerable<Claim>? AccessTokenClaims { get; set; } = null!;
    }
}
