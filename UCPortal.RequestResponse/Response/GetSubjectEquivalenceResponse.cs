using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetSubjectEquivalenceResponse
    {
        public List<Subjects> subjects { get; set; }

        public class Subjects
        {
            public string internal_code { get; set; }
            public string subject_name { get; set; }
            public string descr_1 { get; set; }
            public string descr_2 { get; set; }
            public int units { get; set; }
            public string subject_type {get;set;}
            public string split_code { get; set; }
            public string split_type { get; set; }
            public int semester { get; set; }
            public int year { get; set; }
            public int curr_year { get; set; }
        }
    }
}
