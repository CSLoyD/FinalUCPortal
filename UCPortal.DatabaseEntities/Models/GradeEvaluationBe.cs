using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class GradeEvaluationBe
    {
        public int GradeEvalId { get; set; }
        public string StudId { get; set; }
        public string IntCode { get; set; }
        public string Grade1 { get; set; }
        public string Grade2 { get; set; }
        public string Grade3 { get; set; }
        public string Grade4 { get; set; }
        public string Term { get; set; }
    }
}
