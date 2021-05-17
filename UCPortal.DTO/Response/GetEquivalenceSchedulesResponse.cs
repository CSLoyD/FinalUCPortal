using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetEquivalenceSchedulesResponse
    {
        public List<Schedules> schedules { get; set; }
        public class Schedules
        {
            public string internal_code { get; set; }
            public string edp_code { get; set; }
            public string subject_code { get; set; }
            public string subject_type { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string mdn { get; set; }
            public string days { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
            public string course_code { get; set; }
            public string section { get; set; }
            public string room { get; set; }
        }
    }
}
