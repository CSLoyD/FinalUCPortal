using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class TmpLogin
    {
        public int TmpLoginId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
