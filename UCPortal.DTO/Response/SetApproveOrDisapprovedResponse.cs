using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class SetApproveOrDisapprovedResponse
    {
        public int success { get; set; }
        public string id_number { get; set; }
        public List<string> edp_code { get; set; }
    }
}
