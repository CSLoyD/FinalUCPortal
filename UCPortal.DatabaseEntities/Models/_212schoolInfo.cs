using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class _212schoolInfo
    {
        public int SchoolInfoId { get; set; }
        public int StudInfoId { get; set; }
        public string ElemName { get; set; }
        public string ElemYear { get; set; }
        public short? ElemLastYear { get; set; }
        public string ElemType { get; set; }
        public string ElemLrnNo { get; set; }
        public string ElemEscStudentId { get; set; }
        public string ElemEscSchoolId { get; set; }
        public string SecName { get; set; }
        public string SecYear { get; set; }
        public short? SecLastYear { get; set; }
        public string SecLastStrand { get; set; }
        public string SecType { get; set; }
        public string SecLrnNo { get; set; }
        public string SecEscStudentId { get; set; }
        public string SecEscSchoolId { get; set; }
        public string ColName { get; set; }
        public string ColYear { get; set; }
        public string ColCourse { get; set; }
        public short? ColLastYear { get; set; }
    }
}
