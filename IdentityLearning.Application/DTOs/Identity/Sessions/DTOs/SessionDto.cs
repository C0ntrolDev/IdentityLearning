using IdentityLearning.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.DTOs
{
    public class SessionDto
    {
        public long Id { get; set; }

        public string? DeviceName { get; set; }
        public string? Location { get; set; }
        public string? DeviceInfo { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
