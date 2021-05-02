using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UCPortal.EmailHandler.EmailTemplate
{
    public class EmailTemplates
    {
        public EmailTemplates()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\TokenSend.html";
            var filePathPassword = $"{AppDomain.CurrentDomain.BaseDirectory}\\ResetPassword.html";
            var fileOfficial = $"{AppDomain.CurrentDomain.BaseDirectory}\\OfficiallyEnrolled.html";

            VerificationCode = File.ReadAllText(filePath);
            ResetPassword = File.ReadAllText(filePathPassword);
            OfficialEnrolled = File.ReadAllText(fileOfficial);
        }
        public string VerificationCode { get; }
        public string ResetPassword { get; }
        public string OfficialEnrolled { get; }
    }
}
