using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetStudentStatusResponse
    {
        public string classification { get; set; }
        public int is_cancelled { get; set; }
        public List<Status> status { get; set; }
        public string needed_payment { get; set; }
        public int pending_promissory { get; set; }
        public int promi_pay { get; set; }
        public int adjustment_open { get; set; }
        public int enrollment_open { get; set; }
        public int has_adjustment { get; set; }
        public class Status
        {
            public int step { get; set; }
            public int done { get; set; }
            public int approved { get; set; }
            public string date { get; set; }
        }
    }
}
