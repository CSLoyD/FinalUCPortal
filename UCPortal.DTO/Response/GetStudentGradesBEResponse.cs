using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetStudentGradesBEResponse
    {
        public List<Grades> grades { get; set; }
        public class Grades
        {
            public string internal_code { get; set; }
            public string id_number { get; set; }
            public string mid_grade { get; set; }
            public string final_grade { get; set; }
        }

    }
}
