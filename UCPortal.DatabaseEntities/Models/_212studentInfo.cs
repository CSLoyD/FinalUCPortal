using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class _212studentInfo
    {
        public int StudInfoId { get; set; }
        public string StudId { get; set; }
        public string CourseCode { get; set; }
        public int YearLevel { get; set; }
        public string Mdn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Religion { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public short StartTerm { get; set; }
        public short IsVerified { get; set; }
        public string Token { get; set; }
        public string Classification { get; set; }
        public string Dept { get; set; }
    }
}
