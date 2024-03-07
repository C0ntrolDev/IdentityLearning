using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.User.DTOs
{
    public class GetUserResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? Description { get; set; }
    }
}
