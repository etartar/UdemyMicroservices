namespace FreeCourse.Services.Order.Application.Dtos
{
    public class CreatedOrderDto
    {
        public CreatedOrderDto(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}
