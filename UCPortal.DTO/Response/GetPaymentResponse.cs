using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetPaymentResponse
    {
        public List<image_file> images { get; set; }
        public class image_file
        {
            public string file_name { get; set; }
            public int file_id { get; set; }
            public int status { get; set; }
        }
    }
}
