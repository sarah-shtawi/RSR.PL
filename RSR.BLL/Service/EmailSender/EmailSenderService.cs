using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RSR.BLL.Service.EmailSender
{
    public  class EmailSenderService :IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async System.Threading.Tasks.Task sendEmail(string email , string subject , string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential( _configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"])
            };
            using var mailMessage = new MailMessage
                               (from: _configuration["EmailSettings:Email"],
                                to: email,
                                subject,
                                htmlMessage
                                )
                               { IsBodyHtml = true };

            await client.SendMailAsync(mailMessage);
        }
    }
  
}
