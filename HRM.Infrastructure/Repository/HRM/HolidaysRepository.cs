using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class HolidaysRepository(AppDbContext dbContext): IHolidaysRepository
    {
        public async Task<Holidays> AddHolidaysAsync(Holidays holodays)
        {
            dbContext.Holidays.Add(holodays);
            await dbContext.SaveChangesAsync();
            return holodays;
        }

        public async Task<Holidays> UpdateHolidaysAsync(int id, Holidays holidays)
        {
            var updateHolidays = await dbContext.Holidays.FirstOrDefaultAsync(x => x.ID == id);
            if (updateHolidays is not null)
            {
                updateHolidays.Title = holidays.Title;
                updateHolidays.Date = holidays.Date;
                updateHolidays.HolidayName = holidays.HolidayName;
                updateHolidays.Description = holidays.Description;
                updateHolidays.IsActive = holidays.IsActive;
                updateHolidays.EntryUseID = holidays.EntryUseID;
                updateHolidays.EntryDate = holidays.EntryDate;
                updateHolidays.UpdateUserID = holidays.UpdateUserID;
                updateHolidays.UpdateDate = holidays.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateHolidays;
            }
            return holidays;
        }

        public async Task<bool> DeleteHolidaysAsync(int id)
        {
            var deleteHolidays = await dbContext.Holidays.FirstOrDefaultAsync(x => x.ID == id);

            if (deleteHolidays is not null)
            {
                dbContext.Holidays.Remove(deleteHolidays);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Holidays> HolidaysGetDataAsync(int id)
        {
            return await dbContext.Holidays.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Holidays>> HolidaysGetAllDataAsync()
        {
            return await dbContext.Holidays.ToListAsync();
        }

        public async Task<List<Holidays>> GetHolidaysByName(string name)
        {
            return await dbContext.Holidays.Where(x => x.HolidayName == name).ToListAsync();
        }
    }
}
