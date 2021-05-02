using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public string EdpCode { get; set; }
        public string Description { get; set; }
        public string InternalCode { get; set; }
        public string SubType { get; set; }
        public short Units { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Mdn { get; set; }
        public string Days { get; set; }
        public string MTimeStart { get; set; }
        public string MTimeEnd { get; set; }
        public string MDays { get; set; }
        public int Size { get; set; }
        public int? PendingEnrolled { get; set; }
        public int? OfficialEnrolled { get; set; }
        public int MaxSize { get; set; }
        public string Instructor { get; set; }
        public string CourseCode { get; set; }
        public string Section { get; set; }
        public string Room { get; set; }
        public string Instructor2 { get; set; }
        public short Deployed { get; set; }
        public short Status { get; set; }
        public string SplitType { get; set; }
        public string SplitCode { get; set; }
        public short? IsGened { get; set; }
        public string ActiveTerm { get; set; }
    }
}
