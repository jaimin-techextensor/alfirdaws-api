using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IInvoiceTypeInterface
    {
        Task<List<InvoiceTypeModel>> GetInvoiceTypeOverview();
        Task<InvoiceTypeModel> GetInvoiceTypeById(int invoiceTypeId);
        Response CreateInvoiceType(InvoiceTypeCreateRequest invoiceTypeRequest);
        Response UpdateInvoiceType(InvoiceTypeUpdateRequest invoiceTypeRequest);
        bool DeleteInvoiceType(int invoiceTypeId);
    }
}

