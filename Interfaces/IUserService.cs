using API_ERP.Common;
using API_ERP.Models;
using API_ERP.Models.DB;

namespace API_ERP.Interfaces
{
    public interface IUserService
    {
        Task<AuthoriseViewModel> Authenticate_Async(WebAPILoginKey data);
    }
}
