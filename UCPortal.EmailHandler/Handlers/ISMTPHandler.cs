using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.EmailHandler.Handlers
{
    public interface ISMTPHandler
    {
        bool SendEmail(EmailDetails emailTo, int type);
    }
}
