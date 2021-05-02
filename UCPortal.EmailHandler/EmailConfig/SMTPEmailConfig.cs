using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.EmailHandler.EmailConfig
{
    public class SMTPEmailConfig
    {
        public string SmtpServer => "smtp.uc.edu.ph";
        public int SmtpPort => 587;
        public string SmtpUsername => "noreply@smtp.uc.edu.ph";
        public string SmtpPassword => "Welcome13@";
    }
}
