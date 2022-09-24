namespace FreeCourse.Web.Models.Orders
{
    public class OrderCreatedViewModel
    {
        public int OrderId { get; set; }
        public string Error { get; set; }
        public bool IsSuccessful { get; set; }

        public OrderCreatedViewModel SetError(string error)
        {
            IsSuccessful = false;
            Error = error;
            return this;
        }
    }
}
