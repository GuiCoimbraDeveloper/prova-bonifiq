using ProvaPub.MethodPayments;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class OrderService
    {
        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            switch (paymentMethod)
            {
                case "pix":
                    new PixPayment().Pay();
                    break;
                case "creditcard":
                    new CreditCardPayment().Pay();
                    break;
                case "paypal":
                    new PayPalPayment().Pay();
                    break;
                default:
                    break;
            }

            return await Task.FromResult(new Order()
            {
                Value = paymentValue
            });
        }
    }
}
