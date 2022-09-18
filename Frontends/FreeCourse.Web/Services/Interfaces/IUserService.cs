using FreeCourse.Web.Models.User;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
