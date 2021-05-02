using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GradeReportResponse
    {
        public List<grade_report> gradeR;

        public class grade_report
        { 
            public string stud_id { get; set; }
            public string lastname { get; set; }
            public string firstname { get; set; }
            public string deadline { get; set; }
            public int is_overdue { get; set; }
            public subjects total_subjects { get; set; }
            public subjects submitted { get; set; }
            public subjects not_submitted { get; set; }
            public subjects late_submitted { get; set; }
        }

        public class subjects
        { 
            public int count { get; set; }
            public List<string> subjs { get; set; }
        }
    }
}
