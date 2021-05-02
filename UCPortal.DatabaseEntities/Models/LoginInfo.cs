using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class LoginInfo
    {
        public int CinfoId { get; set; }
        public string StudId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Mi { get; set; }
        public string Suffix { get; set; }
        public string StartTerm { get; set; }
        public string Password { get; set; }
        public string Dept { get; set; }
        public string CourseCode { get; set; }
        public short? Year { get; set; }
        public string Sex { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Facebook { get; set; }
        public short? IsVerified { get; set; }
        public short? IsBlocked { get; set; }
        public short? AllowedUnits { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public short? CurrYear { get; set; }
    }
}
