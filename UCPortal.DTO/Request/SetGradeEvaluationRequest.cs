﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class SetGradeEvaluationRequest
    {
        public string internal_code { get; set; }
        public string id_number { get; set; }
        public  string grade {get;set; }
        public string term { get; set; }
    }
}