using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseLog
    {
        public int CaseLogId { get; set; }
        public int? CaseId { get; set; }
        public int? UserId { get; set; }
        public string? Action { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Case CaseLogNavigation { get; set; } = null!;
    }
}
