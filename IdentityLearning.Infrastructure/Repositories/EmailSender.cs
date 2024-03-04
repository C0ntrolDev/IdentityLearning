using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace IdentityLearning.Infrastructure.Repositories
{
    internal class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly EmailServiceOptions _emailServiceOptions;

        public EmailSender(IOptions<EmailServiceOptions> emailHandlerOptions, ILogger<EmailSender> _logger)
        {
            this._logger = _logger;
            _emailServiceOptions = emailHandlerOptions.Value;
        }


        public void SendEmailConfirmationMessage(string toEmail, string code, long userId)
        {
            Task.Run(async () =>
            {
                var url = _emailServiceOptions.EmailConfirmationUrl +
                          $"?userId={userId}&code={HttpUtility.UrlEncode(code)}";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailServiceOptions.FromName, _emailServiceOptions.FromEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));

                message.Subject = "Confirm your email";
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n  <meta charset=\"UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n\r\n  <style>\r\n    .bg {{\r\n      background-color: #323232;\r\n      padding-top: 20px;\r\n      padding-left: 10px;\r\n      padding-right: 10px;\r\n      height: 500px;\r\n    }}\r\n\r\n    .downBg {{\r\n      background-color: #686868;\r\n      padding: 10px;\r\n    }}\r\n\r\n    .icon {{\r\n      font-size: 15px;\r\n      float: right;\r\n      font-weight: 800;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .dev {{\r\n      font-size: 15px;\r\n      text-align: left;\r\n      display: inline-block;\r\n      font-weight: 400;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .project {{\r\n      font-size: 27px;\r\n      text-align: center;\r\n      font-weight: 800;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .button {{\r\n      font-size: 16px;\r\n      font-weight: 700;\r\n      padding: 15px 40px;\r\n      color: #fff;\r\n      background-color: #2595ff;\r\n      border-color: #0b89ff;\r\n      text-decoration: none;\r\n      display: block;\r\n      text-align: center;\r\n      margin-top: 30px;\r\n      background-image: none;\r\n      font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;\r\n      border: 1px solid transparent;\r\n      white-space: nowrap;\r\n      line-height: 2;\r\n      border-radius: 16px;\r\n    }}\r\n  </style>\r\n\r\n</head>\r\n\r\n<body>\r\n  <div class=\"bg\">\r\n    <h2 class=\"project\">\r\n      IdentityLearning\r\n    </h2>\r\n    <a href=\"{url}\" class=\"button\">\r\n      <b>\r\n        please confirm your email\r\n      </b>\r\n    </a>\r\n\r\n  </div>\r\n  <div class=\"downBg\">\r\n    <p class=\"dev\">\r\n      ControlDev\r\n    </p>\r\n\r\n    <p class=\"icon\">\r\n      ^_^\r\n    </p>\r\n  </div>\r\n\r\n</body>\r\n\r\n</html>"
                };

                var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_emailServiceOptions.AuthenticateEmail, _emailServiceOptions.AuthenticatePassword);

                _logger.LogInformation($"Confirmation Email for user with id: {userId} and code: {code} was sent");
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
                client.Dispose();
            });
        }

        public void SendTwoFactorMessage(string toEmail, string code)
        {
            Console.WriteLine($"TwoFactorCode: {code}");
        }

        public void SendConfirmPasswordChangeMessage(ConfirmPasswordChangeMessage confirmPasswordChangeMessage)
        {
            Task.Run(async () =>
            {
                var url = _emailServiceOptions.EmailConfirmationUrl +
                          $"?userId={confirmPasswordChangeMessage.UserId}&code={HttpUtility.UrlEncode(confirmPasswordChangeMessage.Code)}&newPassword={confirmPasswordChangeMessage.NewPassword}";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailServiceOptions.FromName, _emailServiceOptions.FromEmail));
                message.To.Add(MailboxAddress.Parse(confirmPasswordChangeMessage.ToEmail));

                message.Subject = "Confirm your email";
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n  <meta charset=\"UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n\r\n  <style>\r\n    .bg {{\r\n      background-color: #323232;\r\n      padding-top: 20px;\r\n      padding-left: 10px;\r\n      padding-right: 10px;\r\n      height: 500px;\r\n    }}\r\n\r\n    .downBg {{\r\n      background-color: #686868;\r\n      padding: 10px;\r\n    }}\r\n\r\n    .icon {{\r\n      font-size: 15px;\r\n      float: right;\r\n      font-weight: 800;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .dev {{\r\n      font-size: 15px;\r\n      text-align: left;\r\n      display: inline-block;\r\n      font-weight: 400;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .project {{\r\n      font-size: 27px;\r\n      text-align: center;\r\n      font-weight: 800;\r\n      font-family: Verdana, Geneva, Tahoma, sans-serif;\r\n      color: #fff;\r\n    }}\r\n\r\n    .button {{\r\n      font-size: 16px;\r\n      font-weight: 700;\r\n      padding: 15px 40px;\r\n      color: #fff;\r\n      background-color: #2595ff;\r\n      border-color: #0b89ff;\r\n      text-decoration: none;\r\n      display: block;\r\n      text-align: center;\r\n      margin-top: 30px;\r\n      background-image: none;\r\n      font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;\r\n      border: 1px solid transparent;\r\n      white-space: nowrap;\r\n      line-height: 2;\r\n      border-radius: 16px;\r\n    }}\r\n  </style>\r\n\r\n</head>\r\n\r\n<body>\r\n  <div class=\"bg\">\r\n    <h2 class=\"project\">\r\n      IdentityLearning\r\n    </h2>\r\n    <a href=\"{url}\" class=\"button\">\r\n      <b>\r\n        please confirm your password change\r\n      </b>\r\n    </a>\r\n\r\n  </div>\r\n  <div class=\"downBg\">\r\n    <p class=\"dev\">\r\n      ControlDev\r\n    </p>\r\n\r\n    <p class=\"icon\">\r\n      ^_^\r\n    </p>\r\n  </div>\r\n\r\n</body>\r\n\r\n</html>"
                };

                var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_emailServiceOptions.AuthenticateEmail, _emailServiceOptions.AuthenticatePassword);

                _logger.LogInformation($"Password change confrimation message for user with id: {confirmPasswordChangeMessage.UserId} and code: {confirmPasswordChangeMessage.Code} was sent");
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
                client.Dispose();
            });
        }
    }
}
