using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStudentGradesResponse
    {
        public List<Grades> grades { get; set; }
        public List<Equivalence> equivalence { get; set; }
        public class Grades
        { 
            public string internal_code { get; set; }
            public string id_number { get; set; }
            public string mid_grade { get; set; }
            public string final_grade { get; set; }
        }

        public class Equivalence 
        {
            public string internal_code { get; set; }
            public string equival_code { get; set; }
            public string mid_grade { get; set; }
            public string final_grade { get; set; }
        }
    }
}
