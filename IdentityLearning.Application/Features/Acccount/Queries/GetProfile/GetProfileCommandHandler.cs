using AutoMapper;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Queries.GetProfile
{
    internal class GetProfileCommandHandler : IRequestHandler<GetProfileCommand, Result<ProfileDto>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly IMapper _mapper;

        public GetProfileCommandHandler(IAccountUserRepository accountUserUserRepository, IMapper mapper)
        {
            _accountUserUserRepository = accountUserUserRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<ProfileDto>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var profile = _mapper.Map<ProfileDto>(user);
            return Result<ProfileDto>.Successfull(profile);
        }
    }
}
