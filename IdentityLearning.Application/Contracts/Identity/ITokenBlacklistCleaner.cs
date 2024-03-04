using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.Contracts.Identity
{
    public interface ITokenBlacklistCleaner
    {
        public Task CallClean();
        public void StartPeriodicClean();
        public void StopPeriodicClean();
    }
}
