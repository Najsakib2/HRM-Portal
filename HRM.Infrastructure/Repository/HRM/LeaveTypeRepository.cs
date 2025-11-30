using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class LeaveTypeRepository(AppDbContext dbContext) : ILeaveTypeRepository
    {
        public async Task<LeaveType> AddLeaveTypeAsync(LeaveType leaveType)
        {
            dbContext.LeaveType.Add(leaveType);
            await dbContext.SaveChangesAsync();
            return leaveType;
        }

        public async Task<LeaveType> UpdateLeaveTypeAsync(int id, LeaveType leaveType)
        {
            var updateLeaveType = await dbContext.LeaveType.FirstOrDefaultAsync(x => x.ID == id);
            if (updateLeaveType is not null)
            {
                updateLeaveType.MedicalLeave = leaveType.MedicalLeave;
                updateLeaveType.AnnualLeave = leaveType.AnnualLeave;
                updateLeaveType.CasualLeave = leaveType.CasualLeave;
                updateLeaveType.EntryUseID = leaveType.EntryUseID;
                updateLeaveType.EntryDate = leaveType.EntryDate;
                updateLeaveType.UpdateUserID = leaveType.UpdateUserID;
                updateLeaveType.UpdateDate = leaveType.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateLeaveType;
            }
            return leaveType;
        }

        public async Task<bool> DeleteLeaveTypeAsync(int id)
        {
            var deleteLeaveType = await dbContext.LeaveType.FirstOrDefaultAsync(x => x.ID == id);

            if (deleteLeaveType is not null)
            {
                dbContext.LeaveType.Remove(deleteLeaveType);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<LeaveType> LeaveTypeGetDataAsync(int id)
        {
            return await dbContext.LeaveType.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<LeaveType>> LeaveTypeGetAllDataAsync()
        {
            return await dbContext.LeaveType.ToListAsync();
        }

        //public async Task<List<LeaveType>> GetLeaveTypeByName(string name)
        //{
        //    return await dbContext.LeaveType.Where(x => x.HolidayName == name).ToListAsync();
        //}
    }
}
