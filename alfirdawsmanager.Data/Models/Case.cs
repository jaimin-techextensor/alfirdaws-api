using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Case
    {
        public int CaseId { get; set; }
        public int? CaseTypeId { get; set; }
        public int? CaseCategoryId { get; set; }
        public int? CaseSeverityId { get; set; }
        public int? CasePriorityId { get; set; }
        public int? CaseStatusId { get; set; }
        public int? CaseOriginId { get; set; }
        public int? CaseStatusReasonId { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Resolution { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string? Duration { get; set; }

        public virtual CaseCategory? CaseCategory { get; set; }
        public virtual CaseOrigin? CaseOrigin { get; set; }
        public virtual CasePriority? CasePriority { get; set; }
        public virtual CaseSeverity? CaseSeverity { get; set; }
        public virtual CaseStatus? CaseStatus { get; set; }
        public virtual CaseStatusReason? CaseStatusReason { get; set; }
        public virtual CaseType? CaseType { get; set; }
        public virtual User? User { get; set; }
        public virtual CaseLog CaseLog { get; set; } = null!;
    }
}
