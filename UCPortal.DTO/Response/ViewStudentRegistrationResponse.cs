using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ViewStudentRegistrationResponse
    {
        public string stud_id { get; set; }
        public int allowed_units { get; set;  }
        public string college { get; set; }
        public string course { get; set; }
        public string course_code { get; set; }
        public string assigned_section { get; set; }
        public int year_level { get; set; }
        public string mdn { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string suffix { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string nationality { get; set; }
        public string birthdate { get; set; }
        public string birthplace { get; set; }
        public string religion { get; set; }
        public int start_term { get; set; }
        public int is_verified { get; set; }
        public string classification { get; set; }
        public string dept { get; set; }
        public string pcountry { get; set; }
        public string pprovince { get; set; }
        public string pcity { get; set; }
        public string pbarangay { get; set; }
        public string pstreet { get; set; }
        public string pzip { get; set; }
        public string cprovince { get; set; }
        public string ccity { get; set; }
        public string cbarangay { get; set; }
        public string cstreet { get; set; }
        public string mobile { get; set; }
        public string landline { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string father_name { get; set; }
        public string father_contact { get; set; }
        public string father_occupation { get; set; }
        public string mother_name { get; set; }
        public string mother_contact { get; set; }
        public string mother_occupation { get; set; }
        public string guardian_name { get; set; }
        public string guardian_contact { get; set; }
        public string guardian_occupation { get; set; }
        public string elem_name { get; set; }
        public string elem_year { get; set; }
        public int elem_last_year { get; set; }
        public string elem_type { get; set; }
        public string elem_lrn_number { get; set; }
        public string elem_esc_student_id { get; set; }
        public string elem_esc_school_id { get; set; }
        public string sec_name { get; set; }
        public string sec_year { get; set; }
        public int sec_last_year { get; set; }
        public string sec_last_strand { get; set; }
        public string sec_type { get; set; }
        public string sec_lrn_number { get; set; }
        public string sec_esc_student_id { get; set; }
        public string sec_esc_school_id { get; set; }
        public string col_name { get; set; }
        public string col_year { get; set; }
        public string col_course { get; set; }
        public int col_last_year { get; set; }

        public int request_deblock { get; set; }
        public int request_overload { get; set; }
        public List<attachment> attachments { get; set; }

        public class attachment
        {
            public int attachment_id { get; set; }
            public string id_number { get; set; }
            public string email { get; set; }
            public string type { get; set; }
            public string filename { get; set; }
        }
    }
}
