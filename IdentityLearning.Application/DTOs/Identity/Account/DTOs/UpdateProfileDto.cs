using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.User.DTOs
{
    public class UpdateProfileDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
