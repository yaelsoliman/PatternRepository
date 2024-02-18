using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepositroy.Infrastructure.Shared
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailSetting;
        public EmailService(IOptions<EmailSetting> options)
        {
            this.emailSetting = options.Value;

        }
        public async Task SendEmailAsync(Mailrequest mailrequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSetting.Email);
            email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
            email.Subject = mailrequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailrequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            await smtp.ConnectAsync(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailSetting.UserName, emailSetting.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
