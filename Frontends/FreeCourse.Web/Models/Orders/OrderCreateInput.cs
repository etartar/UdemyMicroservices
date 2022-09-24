using FreeCourse.Web.Models.Baskets;
using System.Collections.Generic;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderCreateInput
    {
        public OrderCreateInput()
        {
            OrderItems = new List<OrderItemCreateInput>();
            Address = new AddressCreateInput();
        }

        public string BuyerId { get; set; }
        public List<OrderItemCreateInput> OrderItems { get; set; }
        public AddressCreateInput Address { get; set; }

        public OrderCreateInput SetAddress(string province, string district, string street, string zipCode, string line)
        {
            Address.Province = province;
            Address.District = district;
            Address.Street = street;
            Address.ZipCode = zipCode;
            Address.Line = line;

            return this;
        }

        public OrderCreateInput SetOrderItems(List<BasketItemViewModel> basketItems)
        {
            basketItems.ForEach(x =>
            {
                OrderItems.Add(new OrderItemCreateInput().SetOrderItem(x.CourseId, x.CourseName, x.GetCurrentPrice));
            });

            return this;
        }
    }
}
