using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.User.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.UpdateProfile
{
    internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<object>>
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IApplicationUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public  async Task<Result<object>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProfileDtoValidator();
            var validateResult = await validator.ValidateAsync(request.UpdateProfileDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            user = _mapper.Map(request.UpdateProfileDto, user);
            await _userRepository.UpdateUser(user);

            return Result<object>.Successfull(null!);
        }
    }
}
