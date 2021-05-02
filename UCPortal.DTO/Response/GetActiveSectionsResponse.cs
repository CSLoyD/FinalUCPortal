using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetActiveSectionsResponse
    {        
        public List<String> sections { get; set; }
    }
}
