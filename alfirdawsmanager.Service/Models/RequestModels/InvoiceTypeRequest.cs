using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class InvoiceTypeCreateRequest
    {
        public string Name { get; set; }
    }

    public class InvoiceTypeUpdateRequest
    {
        public int InvoiceTypeId { get; set; }
        public string Name { get; set; }
    }
}

