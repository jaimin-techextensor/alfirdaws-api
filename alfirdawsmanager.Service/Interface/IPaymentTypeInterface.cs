using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IPaymentTypeInterface
    {
        Task<List<PaymentTypeModel>> GetPaymentTypeOverview();
        Task<PaymentTypeModel> GetPaymentTypeById(int paymentTypeId);
        Response CreatePaymentType(PaymentTypeCreateRequest paymentTypeRequest);
        Response UpdatePaymentType(PaymentTypeUpdateRequest paymentTypeRequest);
        bool DeletePaymentType(int paymentTypeId);
    }
}

