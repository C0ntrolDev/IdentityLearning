using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.User.DTOs
{
    public class UpdatePasswordDto
    {
        public string NewPassword { get; set; } = null!;
    }
}
