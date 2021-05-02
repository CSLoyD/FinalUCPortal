using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStatusCountResponse
    {
        public int registered { get; set; }
        public int approved_registration_registrar { get; set; }
        public int disapproved_registration_registrar { get; set; }
        public int approved_registration_dean { get; set; }
        public int disapproved_registration_dean { get; set; }
        public int selecting_subjects { get; set; }
        public int approved_by_dean { get; set; }
        public int disapproved_by_dean { get; set; }
        public int approved_by_accounting { get; set; }
        public int approved_by_cashier { get; set; }
        public int officially_enrolled { get; set; }
        public int withdrawn_enrollment_before_start_of_class { get; set; }
        public int withdrawn_enrollment_start_of_class { get; set; }
        public int cancelled { get; set; }
        public int request { get; set; }
        public int accounting_count { get; set; }
        public int pending_promissory { get; set; }
        public int approved_promissory { get; set; }
        public int pending_adjustment { get; set; }
        public int approved_adjustment { get; set; }
        public int disapproved_adjustment { get; set; }
        public int pending_prelim { get; set; }
        public int approve_prelim { get; set; }
        public int pending_midterm { get; set; }
        public int approve_midterm { get; set; }
        public int pending_semi { get; set; }
        public int approve_semi { get; set; }
        public int pending_final { get; set; }
        public int approve_final { get; set; }
        public int ack_receipts { get; set; }
        public int notaack_receipts { get; set; }
    }
}
