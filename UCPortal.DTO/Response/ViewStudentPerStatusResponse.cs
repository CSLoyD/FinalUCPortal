using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
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
            public string classification_abbr { get; set; }
            public string course_year { get; set; }
            public int year_level { get; set; }
            public string course_code { get; set; }
            public object status { get; set; }
            public DateTime? enrollmentDate { get; set; }
            public int units { get; set; }
            public DateTime? registered_on { get; set; }
            public DateTime? submitted_on { get; set; }
            public String approved_reg_registrar { get; set; }
            public DateTime? approved_reg_registrar_on { get; set; }
            public string disapproved_reg_registrar { get; set; }
            public DateTime? disaproved_reg_registrar_on { get; set; }
            public string approved_dean_reg { get; set; }
            public DateTime? approved_dean_reg_on { get; set; }
            public string disapproved_reg_dean { get; set; }
            public DateTime? disapproved_reg_dean_on { get; set; }
            public string approved_dean { get; set; }
            public DateTime? approved_dean_on { get; set; }
            public string disapproved_dean { get; set; }
            public DateTime? disapproved_dean_on { get; set; }
            public string approved_accounting { get; set; }
            public DateTime? approved_accounting_on { get; set; }
            public string approved_cashier { get; set; }
            public DateTime? approved_cashier_on { get; set; }
            public int request_deblock { get; set; }
            public int request_overload { get; set; }
            public int needed_payment { get; set; } 
            public int has_payment  { get; set; }
            public int has_promissory { get; set; }
            public int promi_pay { get; set; }
            public string profile { get; set; }
            //public int curr_year { get; set; }
        }
    }
}
