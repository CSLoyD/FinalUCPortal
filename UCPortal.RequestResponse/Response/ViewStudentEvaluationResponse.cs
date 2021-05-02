using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewStudentEvaluationResponse
    {
        public List<Grades> studentGrades { get; set; }
        public class Grades
        {
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string descriptive { get; set; }
            public string midterm_grade { get; set; }
            public string final_grade { get; set; }
            public int units { get; set; }
            public string term { get; set; }
            public string int_code { get; set; }

        }
    }
}
