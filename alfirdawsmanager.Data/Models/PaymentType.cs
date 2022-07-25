using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }
    }
}
