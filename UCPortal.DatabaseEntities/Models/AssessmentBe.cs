using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class AssessmentBe
    {
        public int AssessBeId { get; set; }
        public string StudId { get; set; }
        public double OldAccount { get; set; }
        public double FeeTuition { get; set; }
        public double FeeLab { get; set; }
        public double FeeReg { get; set; }
        public double FeeMiscOthers { get; set; }
        public double FeeTotal { get; set; }
        public double TotalDue { get; set; }
        public double Payment { get; set; }
        public double Discount { get; set; }
        public double Adjustment { get; set; }
        public double Balance { get; set; }
        public double StudShare { get; set; }
        public double StudShareBal { get; set; }
        public double AmountDue { get; set; }
        public string Exam { get; set; }
        public string ActiveTerm { get; set; }
    }
}
