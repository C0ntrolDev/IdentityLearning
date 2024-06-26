﻿using AutoMapper;
using IdentityLearning.Application.DTOs.Identity.Account.Converters;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.DTOs.Identity.Common.DTOs;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, ApplicationUserSessionInfo>();
            CreateMap<RefreshAndAccessTokenResponse, RefreshAndAccessTokenDto>();
            CreateMap<UpdateProfileDto, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileDto>();
            CreateMap<RegisterDto, ApplicationUser>().ConvertUsing<RegisterDtoToApplicationUserConverter>();
            CreateMap<ApplicationUserSession, SessionDto>();

            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, GetUserResponseDto>();
        }
    }
}
