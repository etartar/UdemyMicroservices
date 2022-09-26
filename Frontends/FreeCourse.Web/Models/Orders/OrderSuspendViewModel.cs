namespace FreeCourse.Web.Models.Orders
{
    public class OrderSuspendViewModel
    {
        public string Error { get; set; }
        public bool IsSuccessful { get; set; }

        public OrderSuspendViewModel SetError(string error)
        {
            IsSuccessful = false;
            Error = error;
            return this;
        }
    }
}
