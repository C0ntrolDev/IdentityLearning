using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.DTOs
{
    public class DeleteUserSessionDto
    {
        public long? UserId { get; set; } = null!;
        public long? SessionId { get; set; } = null!;
    }
}
