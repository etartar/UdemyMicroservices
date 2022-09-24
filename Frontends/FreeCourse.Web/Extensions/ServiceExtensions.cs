using FreeCourse.Web.Handler;
using FreeCourse.Web.Models.Settings;
using FreeCourse.Web.Services.Interfaces;
using FreeCourse.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;

namespace FreeCourse.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            services.AddHttpClientCredentialServices(serviceApiSettings);
            services.AddHttpClientResourceOwnerServices(serviceApiSettings);
        }

        private static void AddHttpClientCredentialServices(this IServiceCollection services, ServiceApiSettings serviceApiSettings)
        {
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
        }

        private static void AddHttpClientResourceOwnerServices(this IServiceCollection services, ServiceApiSettings serviceApiSettings)
        {
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IPaymentService, PaymentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
