
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;


namespace identity_Core.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel email);
    }//czlf lpkk cymg lpld
    public class EmailSender : IEmailSender
    {
        private readonly GmailOptions _gmailOptions;

        public EmailSender(IOptions<GmailOptions> options)
        {
            _gmailOptions = options.Value;
        }

        public async Task SendEmailAsync(EmailModel email)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(_gmailOptions.Email),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email.To);

            using var smtp = new SmtpClient
            {
                Host = _gmailOptions.Host,
                Port = _gmailOptions.Port,
                Credentials = new NetworkCredential(
                    _gmailOptions.Email, _gmailOptions.Password),
                UseDefaultCredentials = false,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            await smtp.SendMailAsync(mailMessage);
        }
        public async Task SendEmailAsync2(EmailModel email)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("email Address", "نام نمایشی"),
                To = { email.To },
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };
            SmtpClient smtpServer = new SmtpClient("host", 25) // Host => forExample webmail.codeyad.com
            {
                Credentials = new System.Net.NetworkCredential("userName", "Password"), // UserName == Email
                EnableSsl = false
            };
            smtpServer.Send(mail);
            await Task.CompletedTask;
        }
    }
    /// <summary>
    /// Smtp Ports
    /// </summary>
    // Not-Encrypted 25,
    // Secure Tls 587
    // Secure SSL 465

    public class EmailModel
    {
        public EmailModel(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class GmailOptions
    {
        public const string GmailOptionKey = "GmailOptions";
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
