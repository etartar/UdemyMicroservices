using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();

            ViewBag.Basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput input)
        {
            // 1.yol senkron iletişim
            //var orderStatus = await _orderService.CreateOrder(input);

            // 2.yol asenkron iletişim
            var orderSuspend = await _orderService.SuspendOrder(input);

            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();

                ViewBag.Basket = basket;
                ViewBag.Error = orderSuspend.Error;

                return View(input);
            }

            // 1.yol senkron iletişim
            //return RedirectToAction(nameof(SuccessfulCheckout), new
            //{
            //    orderId = orderStatus.OrderId
            //});

            // 2.yol asenkron iletişim
            return RedirectToAction(nameof(SuccessfulCheckout), new
            {
                orderId = new Random().Next(1, 1000)
            });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.OrderId = orderId;

            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrders());
        }
    }
}
