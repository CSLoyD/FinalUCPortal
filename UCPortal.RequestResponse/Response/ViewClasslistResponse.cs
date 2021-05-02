using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewClasslistResponse
    {
        public string edp_code { get; set; }
        public string subject_name { get; set; }
        public string time_info { get; set; }
        public string units { get; set; }
        public int subject_size { get; set; }
        public string department { get; set; }
        public int official_enrolled_size { get; set; }
        public List<Enrolled> official_enrolled { get; set; }
        public int pending_enrolled_size { get; set; }
        public List<Enrolled> pending_enrolled { get; set; }
        public int not_accepted_section { get; set; }
        public List<Enrolled> not_accepted { get; set; }
        public class Enrolled
        {
            public string id_number { get; set; }
            public string last_name { get; set; }
            public string firstname { get; set; }
            public string course_year { get; set; }
            public string mobile_number { get; set; }
            public string email { get; set; }
            public int status { get; set; }
            public string gender { get; set; }
            public string? grade1 { get; set; }
            public string? grade2 { get; set; }
            public string? grade3 { get; set; }
            public string? grade4 { get; set; }
        }
    }
}
