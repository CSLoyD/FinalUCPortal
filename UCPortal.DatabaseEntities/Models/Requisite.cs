using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Requisite
    {
        public int RequisiteId { get; set; }
        public string InternalCode { get; set; }
        public string RequisiteCode { get; set; }
        public string RequisiteType { get; set; }
    }
}
