using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.Models
{
    public record ConfirmPasswordChangeMessage(string UserId, string Code, string NewPassword, string ToEmail);
}
