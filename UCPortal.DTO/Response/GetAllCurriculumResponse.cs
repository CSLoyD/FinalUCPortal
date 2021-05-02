using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetAllCurriculumResponse
    {
        public List<SchoolYear> year { get; set; }
        public List<Courses> courses { get; set; }
        public List<Departments> departments { get; set; }
        public int current_curriculum { get; set; }
        public class SchoolYear
        {
            public int year { get; set; }
            public int isDeployed { get; set; }
        }
        public class Courses
        {
            public string course_code { get; set; }
            public string course_description { get; set; }
            public string course_abbr { get; set; }
            public int course_year_limit { get; set; }
            public string course_department { get; set; }
            public string course_department_abbr { get; set; }
            public string department { get; set; }
            public int course_active { get; set; }
            public int enrollment_open { get; set; }
        }
        public class Departments
        {
            public string course_department { get; set; }
            public string course_department_abbr { get; set; }
            public string department { get; set; }
        }
    }
}
