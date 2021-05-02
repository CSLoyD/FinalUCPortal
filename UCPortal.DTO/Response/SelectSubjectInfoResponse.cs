using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class SelectSubjectInfoResponse
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
        public int MaxSize { get; set; }
        public string Section { get; set; }
        public string Room { get; set; }
        public string SplitType { get; set; }
        public string SplitCode { get; set; }
        public short? IsGened { get; set; }
        public int status { get; set; }
    }
}
