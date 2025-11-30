using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class AttendanceStatusRepository(AppDbContext dbContext) : IAttendanceStatusRepository
    {
        public async Task<AttendanceStatus> AddAttendanceStatusAsync(AttendanceStatus attendanceStatus)
        {
            dbContext.AttendanceStatus.Add(attendanceStatus);
            await dbContext.SaveChangesAsync();
            return attendanceStatus;
        }

        public async Task<AttendanceStatus> UpdateAttendanceStatusAsync(int id, AttendanceStatus attendanceStatus)
        {
            var updateAttendanceStatus = await dbContext.AttendanceStatus.FirstOrDefaultAsync(x => x.ID == id);
            if (updateAttendanceStatus is not null)
            {
                updateAttendanceStatus.Present = updateAttendanceStatus.Present;
                updateAttendanceStatus.Absent = updateAttendanceStatus.Absent;
                updateAttendanceStatus.Leave = updateAttendanceStatus.Leave;
                updateAttendanceStatus.EntryUseID = updateAttendanceStatus.EntryUseID;
                updateAttendanceStatus.EntryDate = updateAttendanceStatus.EntryDate;
                updateAttendanceStatus.UpdateUserID = updateAttendanceStatus.UpdateUserID;
                updateAttendanceStatus.UpdateDate = updateAttendanceStatus.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateAttendanceStatus;
            }
            return attendanceStatus;
        }

        public async Task<bool> DeleteAttendanceStatusAsync(int id)
        {
            var deleteAttendanceStatus = await dbContext.Holidays.FirstOrDefaultAsync(x => x.ID == id);

            if (deleteAttendanceStatus is not null)
            {
                dbContext.Holidays.Remove(deleteAttendanceStatus);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<AttendanceStatus> AttendanceStatusGetDataAsync(int id)
        {
            return await dbContext.AttendanceStatus.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<AttendanceStatus>> AttendanceStatusGetAllDataAsync()
        {
            return await dbContext.AttendanceStatus.ToListAsync();
        }

        //public async Task<List<AttendanceStatus>> GetHolidaysByName(string name)
        //{
        //    return await dbContext.AttendanceStatus.Where(x => x.HolidayName == name).ToListAsync();
        //}
    }
}