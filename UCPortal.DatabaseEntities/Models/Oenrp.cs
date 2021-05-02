using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Oenrp
    {
        public int OenrpId { get; set; }
        public string StudId { get; set; }
        public short YearLevel { get; set; }
        public string CourseCode { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public short Units { get; set; }
        public string Classification { get; set; }
        public string Dept { get; set; }
        public string Mdn { get; set; }
        public string Section { get; set; }
        public short Status { get; set; }
        public short? RequestOverload { get; set; }
        public short? RequestDeblock { get; set; }
        public short RequestPromissory { get; set; }
        public int PromiPay { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string ApprovedRegRegistrar { get; set; }
        public DateTime? ApprovedRegRegistrarOn { get; set; }
        public string DisapprovedRegRegistrar { get; set; }
        public DateTime? DisapprovedRegRegistrarOn { get; set; }
        public string ApprovedRegDean { get; set; }
        public DateTime? ApprovedRegDeanOn { get; set; }
        public string DisapprovedRegDean { get; set; }
        public DateTime? DisapprovedRegDeanOn { get; set; }
        public string ApprovedDean { get; set; }
        public DateTime? ApprovedDeanOn { get; set; }
        public string DisapprovedDean { get; set; }
        public DateTime? DisapprovedDeanOn { get; set; }
        public short AdjustmentCount { get; set; }
        public string AdjustmentBy { get; set; }
        public DateTime? AdjustmentOn { get; set; }
        public string ApprovedAcctg { get; set; }
        public DateTime? ApprovedAcctgOn { get; set; }
        public int? NeededPayment { get; set; }
        public string ApprovedCashier { get; set; }
        public DateTime? ApprovedCashierOn { get; set; }
        public string ActiveTerm { get; set; }
    }
}
