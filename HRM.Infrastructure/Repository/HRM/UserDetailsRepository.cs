using HRM.Applicatin;
using HRM.Application;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class UserDetailsRepository(AppDbContext dbContext) : IUserDetailsRepository
    {
        public async Task<UserDetails> AddUserDetailsAsync(UserDetails userDetails)
        {
            dbContext.UserDetails.Add(userDetails);
            await dbContext.SaveChangesAsync();
            return userDetails;
        }

        public async Task<UserDetails> UpdateUserDetailsAsync(int id, UserDetails userDetails)
        {
            //var updateUserDetails = await dbContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            //if (updateUserDetails is not null)
            //{
            //    updateUserDetails.UserID = userDetails.UserID;
            //    updateUserDetails.FirstName = userDetails.FirstName;
            //    updateUserDetails.LastName = userDetails.LastName;
            //    updateUserDetails.Gender = userDetails.Gender;
            //    updateUserDetails.BirthDate = userDetails.BirthDate;
            //    updateUserDetails.NIDNumber = userDetails.NIDNumber;
            //    updateUserDetails.DepartmentID = userDetails.DepartmentID;
            //    updateUserDetails.DesignationID = userDetails.DesignationID;
            //    updateUserDetails.PresentAddress = userDetails.PresentAddress;
            //    updateUserDetails.PermanentAddress = userDetails.PermanentAddress;
            //    updateUserDetails.Image = userDetails.Image;
            //    updateUserDetails.About = userDetails.About;
            //    updateUserDetails.Signature = userDetails.Signature;
            //    updateUserDetails.EntryUserID = userDetails.EntryUserID;
            //    updateUserDetails.EntryDate = userDetails.EntryDate;
            //    updateUserDetails.UpdateUserID = userDetails.UpdateUserID;
            //    updateUserDetails.UpdateDate = userDetails.UpdateDate;
            //    await dbContext.SaveChangesAsync();
            //    return updateUserDetails;
            //}
            //return updateUserDetails;

            await dbContext.SaveChangesAsync();
            return userDetails;
        }

        public async Task<bool> DeleteUserDetailsAsync(int id)
        {
            var deleteUserDetails = await dbContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (deleteUserDetails is not null)
            {
                dbContext.UserDetails.Remove(deleteUserDetails);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<UserDetails?> UserDetailsGetDataAsync(int id)
        {
            return await dbContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserDetails?> UserDetailsGetDataByUserIDAsync(int userId)
        {

            return await dbContext.UserDetails
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .FirstOrDefaultAsync(x => x.UserID == userId);
        }

        public async Task<IEnumerable<UserDetails>> UserDetailsGetAllDataAsync()
        {
            return await dbContext.UserDetails.ToListAsync();
        }
    }
}
