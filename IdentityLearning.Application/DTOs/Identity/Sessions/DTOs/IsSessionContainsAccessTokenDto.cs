using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.DTOs
{
    public class IsSessionContainsAccessTokenDto
    {
        public long? SessionId { get; set; } = null!;
        public IEnumerable<Claim>? AccessTokenClaims { get; set; } = null!;
    }
}
