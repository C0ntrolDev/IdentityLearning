using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.DTOs.Identity.Sessions.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Queries.GetUserSessions
{
    public class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, Result<IEnumerable<SessionDto>>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly IMapper _mapper;

        public GetUserSessionsQueryHandler(ISessionRepository sessionRepository, IMapper mapper, IAccountUserRepository accountUserUserRepository)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _accountUserUserRepository = accountUserUserRepository;
        }

        public async Task<Result<IEnumerable<SessionDto>>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<IEnumerable<SessionDto>>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var userSessions = await _sessionRepository.GetUserSessions(request.UserId);
            if (userSessions == null)
            {
                return Result<IEnumerable<SessionDto>>.Successfull(new List<SessionDto>());
            }

            var mappedUserSessions = userSessions.Select(us => _mapper.Map<SessionDto>(us));
            return Result<IEnumerable<SessionDto>>.Successfull(mappedUserSessions);
        }
    }
}
