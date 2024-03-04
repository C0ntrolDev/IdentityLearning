using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Domain.Entities.User;

namespace IdentityLearning.Application.DTOs.Identity.User.Converters
{
    internal class RegisterDtoToApplicationUserConverter : ITypeConverter<RegisterDto, ApplicationUser>
    {
        public ApplicationUser Convert(RegisterDto source, ApplicationUser? destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new ApplicationUser();
            }

            destination.Email = source.Email;
            destination.NormalizedEmail = source.Email.ToUpper();

            destination.UserName = source.Email;
            destination.NormalizedUserName = source.Email.ToUpper();

            destination.Name = source.Name;

            return destination;
        }
    }
}
