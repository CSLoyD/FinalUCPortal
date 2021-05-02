using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetStudentPaymentListResponse
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
            public int approved_promi_amount { get; set; }
            public int pending_exam_promi { get; set; }
            public int status { get; set; }
        }
    }
}
