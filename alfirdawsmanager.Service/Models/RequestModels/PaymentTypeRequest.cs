using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class PaymentTypeCreateRequest
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
    }

    public class PaymentTypeUpdateRequest
    {
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
    }
}

