using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class StatusRepository(AppDbContext dbContext) : IStatusRepository
    {
        public async Task<Status> AddStatusAsync(Status status)
        {
            dbContext.Status.Add(status);
            await dbContext.SaveChangesAsync();
            return status;
        }

        public async Task<Status> UpdateStatusAsync(int id, Status status)
        {
            var updateStatus = await dbContext.Status.FirstOrDefaultAsync(x => x.Id == id);
            if (updateStatus is not null)
            {
                updateStatus.Approved = status.Approved;
                updateStatus.Reject = status.Reject;
                updateStatus.Pending = status.Pending;
                updateStatus.EntryUseID = status.EntryUseID;
                updateStatus.EntryDate = status.EntryDate;
                updateStatus.UpdateUserID = status.UpdateUserID;
                updateStatus.UpdateDate = status.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateStatus;
            }
            return status;
        }

        public async Task<bool> DeleteStatusAsync(int id)
        {
            var deleteStatus = await dbContext.Status.FirstOrDefaultAsync(x => x.Id == id);

            if (deleteStatus is not null)
            {
                dbContext.Status.Remove(deleteStatus);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Status> StatusGetDataAsync(int id)
        {
            return await dbContext.Status.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Status>> StatusGetAllDataAsync()
        {
            return await dbContext.Status.ToListAsync();
        }

        //public async Task<List<Status>> GetHolidaysByName(string name)
        //{
        //    return await dbContext.Status.Where(x => x.HolidayName == name).ToListAsync();
        //}
    }
}
