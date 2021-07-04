using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Framework_API.Services
{
    public class Email : IEmail
    {
        private readonly EmailConfiguration _emailConfiguration;

        public Email(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task SendEmail(string email, string subject, string message)
        {
            var receiver = String.IsNullOrEmpty(email) ? _emailConfiguration.Email : email;
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfiguration.Email, "Thiago")
            };

            mailMessage.To.Add(new MailAddress(receiver));
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            using (SmtpClient smtpClient = new SmtpClient(_emailConfiguration.Address, _emailConfiguration.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
