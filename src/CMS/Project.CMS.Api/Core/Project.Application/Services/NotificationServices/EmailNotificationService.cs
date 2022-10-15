
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Project.Domain.Consts;
using Project.Domain.Contracts.Services.NotificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Application.Services.NotificationServices
{
    public class EmailNotificationService : INotificationService
    {
        public async Task Send(string destination, string content, string subject = null)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(EmailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(destination));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = content };

            using var smtp = new SmtpClient();
            smtp.Connect(EmailSettings.Host, EmailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailSettings.Mail, EmailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
