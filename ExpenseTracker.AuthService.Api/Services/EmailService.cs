using ExpenseTracker.AuthService.Api.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace ExpenseTracker.AuthService.Api.Services
{
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
    }   
    public class EmailService(IOptions<EmailSettings> emailSetting) : IEmailService
    {

            private readonly EmailSettings _emailsetting = emailSetting.Value;
            public async Task SendVerificationEmailAsync(string email,string otp)
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Expense Tracker", _emailsetting.From));

                message.To.Add(MailboxAddress.Parse(email));

                message.Subject = "Expense Tracker - Email Verification";

                message.Body = new TextPart("plain") { Text = $"Your Expense tracker verification code is {otp}" };

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_emailsetting.Host,_emailsetting.Port,SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(_emailsetting.Username, _emailsetting.Password);

                await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
