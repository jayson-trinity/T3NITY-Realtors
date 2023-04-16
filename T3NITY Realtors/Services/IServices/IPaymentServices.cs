using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface IPaymentServices
    {
        Task<string> Pay(ListingsViewModel listings, Customer customer);
        Task<bool> VerifyPay(string invoiceId);
    }
}
