using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetCurriculumBEResponse
    {
        public int success { get; set; }
        public string course_code { get; set; }
        public string dept { get; set; }
        public List<Subjects> subjects { get; set; }
        public List<Requisites> requisites { get; set; }
        public List<Grades> grades { get; set; }
        public int units { get; set; }
        public List<Schedules> schedules { get; set; }
        public int curr_year { get; set; }
        public int status { get; set; }
        public class Subjects
        {
            public string internal_code { get; set; }
            public string subject_name { get; set; }
            public string subject_type { get; set; }
            public string descr_1 { get; set; }
            public string descr_2 { get; set; }
            public string units { get; set; }
            public string semester { get; set; }
            public int year_level { get; set; }
            public string course_code { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
        }

        public class Requisites
        { 
            public string internal_code { get; set; }
            public string subject_code { get; set; }
            public string requisites { get; set; }
            public string requisite_type { get; set; }
        }

        public class Grades
        {
            public string internal_code { get; set; }
            public int eval_id { get; set; }
            public string subject_code { get; set; }
            public string final_grade { get; set; }
        }

        public class Schedules
        {
            public string internal_code { get; set; }
            public string edp_code { get; set; }
            public string subject_code { get; set; }
            public string subject_type { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string mdn { get; set; }
            public string days { get; set; }
            public string split_type { get; set; }
            public string split_code { get; set; }
            public string course_code { get; set; }
            public string section { get; set; }
            public string room { get; set; }
            public string course { get; set; }
        }
    }

}
