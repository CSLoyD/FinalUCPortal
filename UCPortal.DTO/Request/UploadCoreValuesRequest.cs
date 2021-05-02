using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class UploadCoreValuesRequest
    {
        public int exam { get; set; }
        public string department { get; set; }
        public List<cores> stud_core { get; set; }
        public string active_term { get; set; }
        public class cores
        {
            public string stud_id { get; set; }
            public string innovation_1 { get; set; }
            public string innovation_2 { get; set; }
            public string innovation_3 { get; set; }
            public string camaraderie { get; set; }
            public string alignment { get; set; }
            public string respect { get; set; }
            public string excellence { get; set; }
        }
    }
}
