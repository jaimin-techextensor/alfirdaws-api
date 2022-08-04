using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CampaignType
    {
        public CampaignType()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int CampaignTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
