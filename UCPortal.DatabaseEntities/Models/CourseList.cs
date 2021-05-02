using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class CourseList
    {
        public short CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public string CourseAbbr { get; set; }
        public short CourseYearLimit { get; set; }
        public string CourseDepartment { get; set; }
        public string CourseDepartmentAbbr { get; set; }
        public string Department { get; set; }
        public short CourseActive { get; set; }
        public short? EnrollmentOpen { get; set; }
        public DateTime? AdjustmentStart { get; set; }
        public DateTime? AdjustmentEnd { get; set; }
        public string ActiveTerm { get; set; }
    }
}
