namespace FreeCourse.Web.Models.Orders
{
    public class OrderItemCreateInput
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public OrderItemCreateInput SetOrderItem(string productId, string productName, decimal price, string pictureUrl = "")
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;

            return this;
        }
    }
}
