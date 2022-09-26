using System;
namespace alfirdawsmanager.Service.Models
{
    public class VatTypeModel
    {
        public int VatTypeId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public string? Description { get; set; }
    }
}

