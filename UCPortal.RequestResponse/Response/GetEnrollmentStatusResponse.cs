using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetEnrollmentStatusResponse
    {
        public List<courseStatus> courseStat { get; set; }
        public class courseStatus
        {
            public string courseName { get; set; }
            public int pending_registered { get; set; }
            public int subject_selection { get; set; }
            public int pending_dean { get; set; }
            public int pending_accounting { get; set; }
            public int pending_payment { get; set; }
            public int pending_cashier { get; set; }
            public int pending_total { get; set; }
            public int official_total { get; set; }
            public int? year_level { get; set; }
        }
    }
}
