using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class ViewGradesResponse
    {
        public List<student_grade> student_grades { get; set; }
        public class student_grade
        {
            public string id_number { get; set; }
            public string lastname { get; set; }
            public string firstname { get; set; }
            public string grade1 { get; set; }
            public string grade2 { get; set; }
            public string? grade3 { get; set; }
            public string? grade4 { get; set; }
        }
    }
}
