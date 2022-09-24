using FreeCourse.Web.Models.Discounts;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
