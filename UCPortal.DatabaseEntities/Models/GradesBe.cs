using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class GradesBe
    {
        public int GradesBeId { get; set; }
        public string StudId { get; set; }
        public string EdpCode { get; set; }
        public DateTime Dte { get; set; }
        public string Grade1 { get; set; }
        public string Grade2 { get; set; }
        public string Grade3 { get; set; }
        public string Grade4 { get; set; }
        public string ActiveTerm { get; set; }
    }
}
