using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Equivalence
    {
        public int EquivalId { get; set; }
        public string InternalCode { get; set; }
        public string EquivalCode { get; set; }
    }
}
