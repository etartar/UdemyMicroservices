using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Models.Payments;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.Get();
            
            var payment = new PaymentInfoInput(input.CardName, input.CardNumber, input.Expiration, input.CVV, basket.TotalPrice);

            var responsePayment = await _paymentService.ReceivePayment(payment);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel().SetError("Ödeme alınamadı.");
            }

            var orderCreateInput = new OrderCreateInput();
            orderCreateInput.SetAddress(input.Province, input.District, input.Street, input.ZipCode, input.Line);
            orderCreateInput.SetOrderItems(basket.BasketItems);

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel().SetError("Sipariş oluşturulamadı.");
            }

            var orderResponse = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderResponse.Data.IsSuccessful = true;

            await _basketService.Delete();

            return orderResponse.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput();
            orderCreateInput.SetAddress(input.Province, input.District, input.Street, input.ZipCode, input.Line);
            orderCreateInput.SetOrderItems(basket.BasketItems);

            var payment = new PaymentInfoInput();
            payment.SetSuspendOrder(input.CardName, input.CardNumber, input.Expiration, input.CVV, basket.TotalPrice, orderCreateInput);

            var responsePayment = await _paymentService.ReceivePayment(payment);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel().SetError("Ödeme alınamadı.");
            }

            return new OrderSuspendViewModel { IsSuccessful = true };
        }
    }
}
