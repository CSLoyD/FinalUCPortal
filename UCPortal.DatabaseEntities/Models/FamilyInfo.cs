using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class FamilyInfo
    {
        public int FamilyInfoId { get; set; }
        public int StudInfoId { get; set; }
        public string FatherName { get; set; }
        public string FatherContact { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherContact { get; set; }
        public string MotherOccupation { get; set; }
        public string GuardianName { get; set; }
        public string GuardianContact { get; set; }
        public string GuardianOccupation { get; set; }
        public string ActiveTerm { get; set; }
    }
}
