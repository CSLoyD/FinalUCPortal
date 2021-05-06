using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class StudentRequest
    {
        public int StudRequestId { get; set; }
        public string StudId { get; set; }
        public string InternalCode { get; set; }
        public string Term { get; set; }
    }
}
