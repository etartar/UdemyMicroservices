using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggregate.Address(context.Message.Address.Province, 
                context.Message.Address.District, 
                context.Message.Address.Street, 
                context.Message.Address.ZipCode, 
                context.Message.Address.Line);

            var newOrder = new Domain.OrderAggregate.Order(context.Message.BuyerId, newAddress);

            context.Message.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _dbContext.Orders.AddAsync(newOrder);

            await _dbContext.SaveChangesAsync();
        }
    }
}
