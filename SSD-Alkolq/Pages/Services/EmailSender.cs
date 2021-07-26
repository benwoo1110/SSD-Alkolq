using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SSD_Alkolq.Pages.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("alkolq.ssd@gmail.com", "Alkolq"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
