using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityLearning.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }
    }
}
