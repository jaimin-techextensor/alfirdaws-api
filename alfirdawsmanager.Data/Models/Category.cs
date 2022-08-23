using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
