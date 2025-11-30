using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IUserDetailsRepository
    {
        Task<UserDetails> AddUserDetailsAsync(UserDetails userDetails);
        Task<UserDetails> UpdateUserDetailsAsync(int userDetailsId, UserDetails userDetails);
        Task<bool> DeleteUserDetailsAsync(int userDetailsId);
        Task<UserDetails?> UserDetailsGetDataAsync(int id);
        Task<UserDetails?> UserDetailsGetDataByUserIDAsync(int userId);
        Task<IEnumerable<UserDetails>> UserDetailsGetAllDataAsync();
    }
}