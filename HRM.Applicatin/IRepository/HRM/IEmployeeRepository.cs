using HRM.Application;
using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IUserRepository
    {
        Task<Users> AddUserAsync(Users user);
        Task SignUpAsync(Company company, Users user);
        Task<Users> UpdateUserAsync(Users existingUser);
        Task<bool> DeleteUserAsync(Users user);
        Task<Users> UserGetDataAsync(int id);
        Task<Users> UserGetDataByEmailAsync(string email);
        Task<IEnumerable<Users>> UserGetDataByCompanyID(int companyId);
        Task<IEnumerable<Users>> UserGetAllDataAsync();
    }
}
