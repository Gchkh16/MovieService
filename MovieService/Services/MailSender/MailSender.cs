using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MovieService.Services.Abstractions;

namespace MovieService.Services.MailSender
{
    public class MailSender : IMailSender
    {
        private readonly MailSenderOptions _options;

        public MailSender(IOptionsSnapshot<MailSenderOptions> options)
        {
            _options = options.Value;
        }


        public async Task SendMailAsync(MailAddress to, string subject, string body, bool isBodyHtml = true)
        {
            var smtpClient = new SmtpClient
            {
                UseDefaultCredentials = false,
                Port = _options.SmtpPort,
                Credentials = new NetworkCredential(_options.SmtpUserName, _options.SmtpPassword),
                EnableSsl = true,
                Host = _options.SmtpHost,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.FromMailAddress),
                To = { to },
                Subject = subject,
                IsBodyHtml = isBodyHtml,
                Body = body
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}