using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class RequestSchedule
    {
        public int RequestId { get; set; }
        public string SubjectName { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Mdn { get; set; }
        public string Days { get; set; }
        public int? Rtype { get; set; }
        public string MTimeStart { get; set; }
        public string MTimeEnd { get; set; }
        public int? Status { get; set; }
        public string InternalCode { get; set; }
        public int? Size { get; set; }
        public string SplitType { get; set; }
        public string SplitCode { get; set; }
    }
}
