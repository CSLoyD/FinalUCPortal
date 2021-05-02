using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.EmailHandler.Enums
{
    public enum EnrollmentStatus
    {
        REGISTERED,
        APPROVED_REGISTRATION_REGISTRAR,
        DISAPPROVED_REGISTRATION_REGISTRAR,
        APPROVED_REGISTRATION_DEAN,
        DISAPPROVED_REGISTRATION_DEAN,
        SELECTING_SUBJECTS,
        APPROVED_BY_DEAN,
        DISAPPROVED_BY_DEAN,
        APPROVED_BY_ACCOUNTING,
        APPROVED_BY_CASHIER,
        OFFICIALLY_ENROLLED,
        WITHDRAWN_ENROLLMENT_BEFORE_START_OF_CLASS,
        WITHDRAWN_ENROLLMENT_START_OF_CLASS,
        CANCELLED
    }

    public enum EmailType
    {
        VERIFICATIONCODE = 1,
        RESETPASSWORD = 2,
        OFFICIALENROLLMENT = 3
    } 
}
