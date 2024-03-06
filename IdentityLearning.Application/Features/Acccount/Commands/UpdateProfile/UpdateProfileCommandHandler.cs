using AutoMapper;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.Account.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.UpdateProfile
{
    internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<object>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IAccountUserRepository accountUserUserRepository, IMapper mapper)
        {
            _accountUserUserRepository = accountUserUserRepository;
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

            var user = await _accountUserUserRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            user = _mapper.Map(request.UpdateProfileDto, user);
            await _accountUserUserRepository.UpdateUser(user);

            return Result<object>.Successfull(null!);
        }
    }
}
