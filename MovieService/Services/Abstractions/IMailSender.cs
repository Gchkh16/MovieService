using System.Net.Mail;
using System.Threading.Tasks;

namespace MovieService.Services.Abstractions
{
    public interface IMailSender
    {
        Task SendMailAsync(MailAddress to, string subject, string body, bool isBodyHtml = true);
    }
}