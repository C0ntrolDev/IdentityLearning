using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.Token.DTOs
{
    public class IsUserClaimsInBlacklistDto
    {
        public IEnumerable<Claim> Claims { get; set; } = null!;
    }
}
