﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Models;

namespace IdentityLearning.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        public void SendEmailConfirmationMessage(string toEmail, string code, long userId);
        public void SendDeleteAccountConfirmationMessage(string toEmail, string code, long userId); }
}
