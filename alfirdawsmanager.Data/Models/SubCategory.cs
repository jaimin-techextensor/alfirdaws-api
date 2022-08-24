﻿using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class SubCategory
    {
        public int SubCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }

        public virtual Category? Category { get; set; }
    }
}
