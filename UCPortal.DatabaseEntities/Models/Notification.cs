using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class Notification
    {
        public int NotifId { get; set; }
        public string StudId { get; set; }
        public short NotifRead { get; set; }
        public string Message { get; set; }
        public DateTime Dte { get; set; }
        public string ActiveTerm { get; set; }
    }
}
