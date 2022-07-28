using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public CreateOrderCommand(string buyerId, List<OrderItemDto> orderItems, AddressDto address)
        {
            BuyerId = buyerId;
            OrderItems = orderItems;
            Address = address;
        }

        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
