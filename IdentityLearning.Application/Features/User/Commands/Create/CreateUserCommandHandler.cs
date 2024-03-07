using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.DTOs.Identity.User.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.Create
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserDtoValidator();
            var validateResult = await validator.ValidateAsync(request.User, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var user = _mapper.Map<ApplicationUser>(request.User);

            await _userRepository.CreateUser(user, request.User.Password);

            return Result<object>.Successfull(null!);
        }
    }
}
