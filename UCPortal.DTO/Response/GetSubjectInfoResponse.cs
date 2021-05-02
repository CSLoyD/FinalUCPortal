using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetSubjectInfoResponse
    {
        public List<Subjects> subjects { get; set; }
        public List<Requisites> requisites { get; set; }
        public class Subjects
        {
            public string internal_code { get; set; }
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string descr_1 { get; set; }
            public string descr_2 { get; set; }
            public string units { get; set; }
            public string semester { get; set; }
            public int year_level { get; set; }
            public string course_code { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
        }
        public class Requisites
        {
            public string internal_code { get; set; }
            public string subject_code { get; set; }
            public string requisites { get; set; }
            public string requisite_type { get; set; }
        }
    }
}
