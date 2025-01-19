using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.Extensions.Options;

namespace Therapi.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _emailSenderOptions;

        public EmailSender(IOptions<EmailSenderOptions> emailSenderOptions)
        {
            _emailSenderOptions = emailSenderOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("TherapiCare", _emailSenderOptions.SmtpCredential.UserName));
            mailMessage.To.Add(new MailboxAddress("Receiver", email));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_emailSenderOptions.SmtpHost, _emailSenderOptions.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_emailSenderOptions.SmtpCredential.UserName, _emailSenderOptions.SmtpCredential.Password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
