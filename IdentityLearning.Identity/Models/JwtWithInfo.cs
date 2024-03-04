using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Entities.User;

namespace IdentityLearning.Identity.Models
{
    public record JwtWithInfo(TokenInfo Info, string Jwt);

}
