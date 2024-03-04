using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IdentityLearning.Application.Features.Common
{
    public class RequestWithUserId<TResponse>(long userId) : IRequest<TResponse>
    {
        public long UserId { get; } = userId;
    }
}
