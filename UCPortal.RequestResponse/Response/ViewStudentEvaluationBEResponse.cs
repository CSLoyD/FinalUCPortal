using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewStudentEvaluationBEResponse
    {
        public List<Grades> studentGrades { get; set; }
        public class Grades
        {
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string descriptive { get; set; }
            public string grade_1 { get; set; }
            public string grade_2 { get; set; }
            public string grade_3 { get; set; }
            public string grade_4 { get; set; }
            public int units { get; set; }
            public string term { get; set; }
            public string int_code { get; set; }

        }
    }
}
