using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetStudyLoadResponse
    {
        public List<Schedules> schedules { get; set; }
        public int has_nstp { get; set; }
        public int has_pe { get; set; }
        public class Schedules
        {
            public string edp_code { get; set; }
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string days { get; set; }
            public string begin_time { get; set; }
            public string end_time { get; set; }
            public string mdn { get; set; }
            public string m_begin_time { get; set; }
            public string m_end_time { get; set; }
            public string m_days { get; set; }
            public int size { get; set; }
            public int max_size { get; set; }
            public int units { get; set; }
            public string room { get; set; }
            public string descriptive_title { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
            public string section { get; set; }
            public string course_abbr { get; set; }
            public int status { get; set;  }
        }
    }

}
