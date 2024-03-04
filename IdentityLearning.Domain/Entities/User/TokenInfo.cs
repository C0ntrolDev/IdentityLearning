using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Domain.Entities.User
{
    public class TokenInfo
    {
        public Guid Id { get; set; }
        public DateTime Expiration { get; set; }

        public long SessionId { get; set; }
        public ApplicationUserSession Session { get; set; } = null!;
    }
}
