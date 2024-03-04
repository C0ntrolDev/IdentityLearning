using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Infrastructure.Options
{
    public class EmailServiceOptions
    {
        public string EmailConfirmationUrl { get; set; } = null!;

        public string FromName { get; set; } = null!;
        public string FromEmail { get; set; } = null!;

        public string AuthenticateEmail { get; set; } = null!;
        public string AuthenticatePassword { get; set; } = null!;
    }
}
