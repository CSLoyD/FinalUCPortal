using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Config
    {
        public int ConfigId { get; set; }
        public int Sequence { get; set; }
        public short IdYear { get; set; }
        public short CampusId { get; set; }
        public string CampusLms { get; set; }
        public DateTime? Prelim { get; set; }
        public DateTime? Midterm { get; set; }
        public DateTime? Semifinal { get; set; }
        public DateTime? Final { get; set; }
        public int BasicStart { get; set; }
        public int BasicEnd { get; set; }
        public int PermitCutoff { get; set; }
        public DateTime? Grade1Due { get; set; }
        public DateTime? Grade2Due { get; set; }
        public string ActiveTerm { get; set; }
        public string ActiveTerms { get; set; }
    }
}
