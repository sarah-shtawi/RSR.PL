using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.EmailSender
{
    public  interface IEmailSenderService
    {
        System.Threading.Tasks.Task sendEmail(string email, string subject, string htmlMessage);
    }
}
