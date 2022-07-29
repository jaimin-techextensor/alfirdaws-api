using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Regions = new HashSet<Region>();
        }

        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? Flag { get; set; }
        public byte[]? Icon { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
