using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Vattype
    {
        public int VattypeId { get; set; }
        public string? Name { get; set; }
        public decimal Percentage { get; set; }
        public string? Description { get; set; }
    }
}
