using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class SetApproveOrDisapprovedRequest
    {
        public string id_number { get; set; }
        public string existing_id_number { get; set; }
        public int status { get; set; }
        public string name_of_approver { get; set; }
        public string message { get; set; }
        public int year_level { get; set; }
        public string classification { get; set; }
        public int allowed_units { get; set; }
        public string course_code { get; set; }
        public string section { get; set; }
        public int needed_payment { get; set; }
        public string mdn { get; set; }
        public int curr_year { get; set; }
        public string active_term { get; set; }
    }
}
