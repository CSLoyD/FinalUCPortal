using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class AssessmentCl
    {
        public int AssessClId { get; set; }
        public string StudId { get; set; }
        public double OldAccount { get; set; }
        public double FeeTuition { get; set; }
        public double FeeLab { get; set; }
        public double FeeReg { get; set; }
        public double FeeMisc { get; set; }
        public double AssessTotal { get; set; }
        public double? ExcessPayment { get; set; }
        public double Payment { get; set; }
        public double Discount { get; set; }
        public double Adjustment { get; set; }
        public double? AdjustmentCredit { get; set; }
        public double? AdjustmentDebit { get; set; }
        public double Balance { get; set; }
        public double AmountDue { get; set; }
        public string Exam { get; set; }
        public string ActiveTerm { get; set; }
    }
}
