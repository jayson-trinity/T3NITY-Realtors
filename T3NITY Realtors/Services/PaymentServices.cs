

using BitPayLight;
using BitPayLight.Models;
using BitPayLight.Models.Invoice;
using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class PaymentServices : IPaymentServices
    {
        protected IDbOperations _DbOperations;
        protected IConfiguration _configuration;

        public PaymentServices(IDbOperations dbOperations, IConfiguration configuration)
        {
            _DbOperations = dbOperations;
            _configuration = configuration;
        }

        public async Task<string> Pay(ListingsViewModel listings, Customer customer)
        {
            BitPay bitpay = new BitPay(_configuration["BitpayKey"], Env.Test);

            var buyerData = new Buyer();
            buyerData.Name = customer.LastName + " " + customer.FirstName;
            buyerData.Address1 = "Nigeria";
            buyerData.Address2 = "Nigeria";
            buyerData.Locality = "Nigeria";
            buyerData.Region = "Nigeria";
            buyerData.PostalCode = "20000";
            buyerData.Country = "Nigeria";
            buyerData.Notify = true;

            Invoice invoice = new Invoice((double)listings.Price, Currency.NGN)
            {
                Buyer = buyerData,
                PosData = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                PaymentCurrencies = new List<string> { Currency.BTC, Currency.BCH, Currency.ETH }
            };
            //creating invocice for payment
            invoice = await bitpay.CreateInvoice(invoice);

            _DbOperations.PaymentsRepository().Add(new Payments()
            {
                Amount = listings.Price,
                InvoiceGiud = invoice.Guid,
                InvoiceId = invoice.Id,
                InvoiceUrl = invoice.Url,
                Status = invoice.Status,
                ListingsId = listings.Id,
                UsersId = customer.UsersId
            });
            //rreturning payment url
            return invoice.Url;
        }

        public async Task<bool> VerifyPay(string invoiceId)
        {
            var payment = _DbOperations.PaymentsRepository().Find(p => p.InvoiceId == invoiceId);
            if (payment != null)
            {
                BitPay bitpay = new BitPay(_configuration["BitpayKey"], Env.Test);
                var invoice = await bitpay.GetInvoice(invoiceId);
                if (invoice != null)
                {
                    payment.Status = invoice.Status;
                    _DbOperations.PaymentsRepository().Update(payment, payment.Id);
                    return true;
                }
            }
            return false;
        }

    }
}
