using System;

namespace MovieService.Services.MailSender
{
    [Serializable]
    public class MailSenderOptions
    {
        public string FromMailAddress { get; set; }

        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
    }
}