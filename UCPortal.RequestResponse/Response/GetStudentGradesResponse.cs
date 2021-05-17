using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStudentGradesResponse
    {
        public List<Grades> grades { get; set; }
        public class Grades
        {
            public string internal_code { get; set; }
            public int eval_id { get; set; }
            public string subject_code { get; set; }
            public string final_grade { get; set; }
        }

    }
}
