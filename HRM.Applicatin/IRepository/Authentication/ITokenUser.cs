
using System.Security.Claims;

namespace HRM.Application.IRepository.Authentication
{
    public interface ITokenUser
    {
        ClaimsPrincipal GetCurrentUser();
        string GetUserId();
        string GetUserCompanyId();
        string GetUserName();
        string GetUserFullName();
        string GetUserEmail();
        string GetUserRole();
        bool IsActive();
    }
}
