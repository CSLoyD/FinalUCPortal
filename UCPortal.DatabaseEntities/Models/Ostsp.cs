using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Ostsp
    {
        public int StsId { get; set; }
        public string StudId { get; set; }
        public string EdpCode { get; set; }
        public short Status { get; set; }
        public string Remarks { get; set; }
        public DateTime AdjustedOn { get; set; }
        public string ActiveTerm { get; set; }
    }
}
