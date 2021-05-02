using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewScheduleResponse
    {
        public List<schedule> schedules { get; set; }
        public int count { get; set; }
        public class schedule
        {
            public string edpcode { get; set; }
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string days { get; set; }
            public string begin_time { get; set; }
            public string end_time { get; set; }
            public string mdn { get; set; }
            public string m_begin_time { get; set; }
            public string m_end_time { get; set; }
            public string m_days { get; set; }
            public string units { get; set; }
            public string room { get; set; }
            public string size { get; set; }
            public int pending_enrolled { get; set; }
            public int official_enrolled { get; set; }
            public string max_size { get; set; }
            public string section { get; set; }
            public int status { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
            public string descriptive_title { get; set; }
            public string course_code { get; set; }
            public int year_level { get; set; }
            public short gened { get; set; }
            public string course_abbr { get; set; }
            public string instructor { get; set; }
        }
    }
}
