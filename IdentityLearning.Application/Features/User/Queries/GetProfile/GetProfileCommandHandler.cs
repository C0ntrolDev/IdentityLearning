using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Queries.GetProfile
{
    internal class GetProfileCommandHandler : IRequestHandler<GetProfileCommand, Result<ProfileDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetProfileCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<ProfileDto>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var profile = _mapper.Map<ProfileDto>(user);
            return Result<ProfileDto>.Successfull(profile);
        }
    }
}
