using FreeCourse.Web.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetOrders();

        /// <summary>
        /// Senkron iletişim - direk order microservisine istek yapılacak.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input);

        /// <summary>
        /// Asenkron iletişim - sipariş bilgileri RabbitMQ'ya gönderilecek.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input);
    }
}
