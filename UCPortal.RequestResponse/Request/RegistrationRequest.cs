using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class RegistrationRequest
    {
        public StudentInfo student_info { get; set; }
        public AddressContact address_contact { get; set; }
        public FamilyInfo family_info { get; set; }
        public SchoolInfo school_info { get; set; }
        public List<Attachment> attachment { get; set; }
        public string active_term { get; set; }

        public class StudentInfo
        {
            [Required]
            public string course { get; set; }
            [Required]
            public int year_level { get; set; }
            public string mdn { get; set; }
            [Required]
            public string first_name { get; set; }
            public string middle_name { get; set; }
            [Required]
            public string last_name { get; set; }
            public string suffix { get; set; }
            [Required]
            public string gender { get; set; }
            [Required]
            public string status { get; set; }
            [Required]
            public string nationality { get; set; }
            [Required]
            public string birthdate { get; set; }
            [Required]
            public string birthplace { get; set; }
            [Required]
            public string religion { get; set; }
            [Required]
            public int start_term { get; set; }
            [Required]
            public int allowed_units { get; set; }
            [Required]
            public string classification { get; set; }
            [Required]
            public string dept { get; set; }
            [Required]
            public string password { get; set; }
        }
        public class AddressContact
        { 
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
            [Required]
            public string mobile { get; set; }
            public string landline { get; set; }
            [Required]
            public string email { get; set; }
            public string facebook { get; set; }
        }
        public class FamilyInfo
        { 
            public string father_name { get; set; }
            public string father_contact { get; set; }
            public string father_occupation { get; set; }
            public string mother_name { get; set; }
            public string mother_contact { get; set; }
            public string mother_occupation { get; set; }
            public string guardian_name { get; set; }
            public string guardian_contact { get; set; }
            public string guardian_occupation { get; set; }
        }
        public class SchoolInfo
        {
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
        }

        public class Attachment
        {
            public string email { get; set; }
            public string type { get; set; }
            public string filename { get; set; }
        }
    }
}
