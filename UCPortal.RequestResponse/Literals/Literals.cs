using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Literals
{
    public class Literals
    {
        public const string REGISTERED = "Step 1: You have successfully registered. Please wait for Approval of Registrar and Dean/Principal to proceed to the next step.";
        public const string APPROVED_REGISTRATION_REGISTRAR = "Step 2: Your registration has been approved by Registrar. Waiting for Dean's Approval";
        public const string DISAPPROVED_REGISTRATION_REGISTRAR = "Step 2: Your Regisration has been declined by Registrar. Reason: ";
        public const string APPROVED_REGISTRATION_DEAN = "Step 3: Your registration has been approved by your Dean.";
        public const string DISAPPROVED_REGISTRATION_DEAN = "Step 3: Your Regisration has been declined by Dean. Reason: ";
        public const string SELECTING_SUBJECTS = "Step 4: Subject has been selected. Wait for approval.";
        public const string APPROVED_BY_DEAN = "Step 5: Your enrollment has been approved by your Dean.";
        public const string DISAPPROVED_BY_DEAN = "Step 5: Your enrollment has been declined by your Dean. Reason: ";
        public const string APPROVED_BY_ACCOUNTING = "Step 6: Your enrollment has been approved by Accounting.";
        public const string APPROVED_BY_CASHIER = "Step 7: Your enrollment has been approved Cashier.";
        public const string OFFICIALLY_ENROLLED = "Step 8: You are now officially enrolled.";
        public const string WITHDRAWN_ENROLLMENT_BEFORE_START_OF_CLASS = "You withdrawn your enrollment before start of class.";
        public const string WITHDRAWN_ENROLLMENT_START_OF_CLASS = "You withdrawn your enrollment during start of class.";
        public const string CANCELLED = "You cancelled your enrollment.";
        public const string APPROVE_DE_BLOCK = "Dean has approve your de-block request.";
        public const string DISAPPROVE_DE_BLOCK = "Dean has declined your de-block request";
        public const string APPROVE_OVERLOAD = "Dean has approve your overload request. New maximum units: ";
        public const string DISAPPROVE_OVERLOAD = "Dean has declined your overlaod request";
        public const string APPROVE_PROMISSORY = "Dean/CAD has approve your promissory request";
        public const string REQUESTED_ADJUSTMENT = "You requested for an adjustment in your schedule";
        public const string DISAPPROVE_ADJUSTMENT = "Dean has declined your adjustment request";
        public const string APPROVE_ADJUSTMENT = "Dean has approve your approved request";
        public const string DISSOLVED_SUBJECT = "You have a dissolved subject: ";
    }
}
