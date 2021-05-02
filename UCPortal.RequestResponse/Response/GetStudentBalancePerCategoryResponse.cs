using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStudentBalancePerCategoryResponse
    {
        public List<student_info> category_1 { get; set; }
        public List<student_info> category_2 { get; set; }
        public List<student_info> category_3 { get; set; }
        public class student_info
        {
            public string id_number { get; set; }
            public string lastname { get; set; }
            public string first_name { get; set; }
            public string course_year { get; set; }
            public double total_assessment { get; set; }
            public double total_balance { get; set; }
            public double due { get; set; }
            public string email { get; set; }
            public string mobile_number { get; set; }
        }
    }
}
