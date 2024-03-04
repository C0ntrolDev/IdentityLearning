using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.User.DTOs
{
    public class ConfirmEmailDto
    { 
        public string ConfirmationCode { get; set; } = null!;
    }
}
