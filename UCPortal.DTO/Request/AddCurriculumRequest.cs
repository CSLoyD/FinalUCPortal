using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class AddCurriculumRequest
    {
        public int curr_year { get; set; }
        public string course { get; set; }
        public List<Subjects> subjects { get; set; }

        public class Subjects
        { 
            public string subject { get; set; }
            public string description { get; set; }
            public int lec { get; set; }
            public int lab { get; set; }
            public int year { get; set; }
            public int semester { get; set; }
        }
    }
}
