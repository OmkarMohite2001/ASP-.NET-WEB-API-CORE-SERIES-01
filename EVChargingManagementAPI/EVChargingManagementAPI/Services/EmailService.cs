using EVChargingManagementAPI.DTOs;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
namespace EVChargingManagementAPI.Services
{
    public class EmailService:IEmailService
    {
        //public async Task SendEmailAsync(string to,string subject,string body)
        //{

        //    var email = new MimeMessage();
        //    email.From.Add(
        //        new MailboxAddress("EV Charging Management", "vikramyadav20988@gmail.com"));
        //    email.To.Add(MailboxAddress.Parse(to));
        //    email.Subject = subject;

        //    email.Body = new TextPart("plain")
        //    {
        //        Text = body
        //    };

        //    using var smtp = new SmtpClient();

        //    await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

        //    await smtp.AuthenticateAsync("vikramyadav20988@gmail.com", "sxjearmfdkpnjjat");
        //    await smtp.SendAsync(email);
        //    await smtp.DisconnectAsync(true);

        //}
        private readonly EmailSettings _emailSettings;

        public EmailService(
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(
            string to,
            string subject,
            string body)
        {
            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    "EV Charging Management",
                    _emailSettings.Email));

            email.To.Add(
                MailboxAddress.Parse(to));

            email.Subject = subject;

            email.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailSettings.Host,
                _emailSettings.Port,
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailSettings.Email,
                _emailSettings.AppPassword);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}
