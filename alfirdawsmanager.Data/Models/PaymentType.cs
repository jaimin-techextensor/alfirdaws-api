﻿using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
    }
}
