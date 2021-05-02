using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class _212examPromissory
    {
        public int ExamPromiId { get; set; }
        public string StudId { get; set; }
        public short? RequestPrelim { get; set; }
        public int? RequestPrelimAmount { get; set; }
        public DateTime? RequestPrelimDate { get; set; }
        public int? PrelimPromiId { get; set; }
        public short? RequestMidterm { get; set; }
        public int? RequestMidtermAmount { get; set; }
        public DateTime? RequestMidtermDate { get; set; }
        public int? MidtermPromiId { get; set; }
        public short? RequestSemi { get; set; }
        public int? RequestSemiAmount { get; set; }
        public DateTime? RequestSemiDate { get; set; }
        public int? SemiPromiId { get; set; }
        public short? RequestFinal { get; set; }
        public int? RequestFinalAmount { get; set; }
        public DateTime? RequestFinalDate { get; set; }
        public int? FinalPromiId { get; set; }
    }
}
