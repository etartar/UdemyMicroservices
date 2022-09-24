namespace FreeCourse.Web.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice;

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }

        public decimal GetCurrentPrice
        {
            get
            {
                return DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
            }
        }
    }
}
