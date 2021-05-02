using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class SubjectInfo
    {
        public int SubInfoId { get; set; }
        public string InternalCode { get; set; }
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
        public string Descr1 { get; set; }
        public string Descr2 { get; set; }
        public short Units { get; set; }
        public short? Semester { get; set; }
        public string CourseCode { get; set; }
        public int? YearLevel { get; set; }
        public string SplitType { get; set; }
        public string SplitCode { get; set; }
        public int? CurriculumYear { get; set; }
    }
}
