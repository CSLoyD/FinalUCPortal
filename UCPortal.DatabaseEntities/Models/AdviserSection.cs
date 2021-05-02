using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class AdviserSection
    {
        public int SectionAdId { get; set; }
        public string Section { get; set; }
        public string Instructor { get; set; }
        public string Department { get; set; }
        public string ActiveTerm { get; set; }
    }
}
