using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class GradeEvaluation
    {
        public int GradeEvalId { get; set; }
        public string StudId { get; set; }
        public string IntCode { get; set; }
        public string MidtermGrade { get; set; }
        public string FinalGrade { get; set; }
        public string Term { get; set; }
    }
}
