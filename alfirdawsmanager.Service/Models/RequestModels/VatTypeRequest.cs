using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class VatTypeCreateRequest
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public string? Description { get; set; }
    }

    public class VatTypeUpdateRequest
    {
        public int VatTypeId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public string? Description { get; set; }
    }
}

