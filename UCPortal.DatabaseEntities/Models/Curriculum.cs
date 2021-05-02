using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Curriculum
    {
        public int CurrId { get; set; }
        public short? Year { get; set; }
        public short? IsDeployed { get; set; }
    }
}
