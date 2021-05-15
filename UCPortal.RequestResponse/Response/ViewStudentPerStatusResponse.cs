using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewStudentPerStatusResponse
    {
        public int count { get; set; }
        public List<Student> students { get; set; }

        public class Student
        {
            public string id_number { get; set; }
            public string lastname { get; set; }
            public string firstname { get; set; }
            public string mi { get; set; }
            public string suffix { get; set; }
            public string classification { get; set; }
            public string course_year { get; set; }
            public string course_code { get; set; }
            public object status { get; set; }
            public string date { get; set; }
            public int request_deblock { get; set; }
            public int request_overload { get; set; }
            public string needed_payment { get; set; }
            public int has_payment { get; set; }
            public int has_promissory { get; set; }
            public int promi_pay { get; set; }
            public string profile { get; set; }
            //public int curr_year { get; set; }
        }
    }
}
