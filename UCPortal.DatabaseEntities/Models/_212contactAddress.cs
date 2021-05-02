using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class _212contactAddress
    {
        public int AddConId { get; set; }
        public int StudInfoId { get; set; }
        public string PCountry { get; set; }
        public string PProvince { get; set; }
        public string PCity { get; set; }
        public string PBarangay { get; set; }
        public string PStreet { get; set; }
        public string PZip { get; set; }
        public string CProvince { get; set; }
        public string CCity { get; set; }
        public string CBarangay { get; set; }
        public string CStreet { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
    }
}
