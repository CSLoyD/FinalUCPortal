using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class ViewAdviserResponse
    {
        public List<Section> sections { get; set; }
        public class Section
        {
            public string section { get; set; }
            public string id_number { get; set; }
            public string name { get; set; }
        }
    }
}
