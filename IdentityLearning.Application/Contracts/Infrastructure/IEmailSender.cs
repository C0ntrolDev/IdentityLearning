using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Models;

namespace IdentityLearning.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        public void SendEmailConfirmationMessage(string toEmail, string code, long userId);
        public void SendTwoFactorMessage(string toEmail, string code);
        public void SendPasswordChangedMessage(string toEmail, string newPassword);
    }
}
