using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Region
    {
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }

        public virtual Country Country { get; set; } = null!;
    }
}
