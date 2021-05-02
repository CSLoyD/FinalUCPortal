using System;
using System.Collections.Generic;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class _212attachment
    {
        public int AttachmentId { get; set; }
        public string StudId { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Filename { get; set; }
        public short? Acknowledged { get; set; }
    }
}
