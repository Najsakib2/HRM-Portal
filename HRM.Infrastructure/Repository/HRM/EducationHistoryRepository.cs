using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class EducationHistoryRepository(AppDbContext dbContext) : IEducationHistoryRepository
    {
        public async Task<EducationHistory> AddEducationHistoryAsync(EducationHistory educationHistory)
        {
            dbContext.EducationHistory.Add(educationHistory);
            await dbContext.SaveChangesAsync();
            return educationHistory;
        }

        public async Task<EducationHistory> UpdateEducationHistoryAsync(int id, EducationHistory educationHistory)
        {
            var updateEducationHistory = await dbContext.EducationHistory.FirstOrDefaultAsync(x => x.ID == id);

            if (updateEducationHistory is not null)
            {
                updateEducationHistory.UserID= updateEducationHistory.UserID;
                updateEducationHistory.Inistute = updateEducationHistory.Inistute;
                updateEducationHistory.Subject = updateEducationHistory.Subject;
                updateEducationHistory.PassingYear = updateEducationHistory.PassingYear;
                updateEducationHistory.EntryUseID = updateEducationHistory.EntryUseID;
                updateEducationHistory.EntryDate = updateEducationHistory.EntryDate;
                updateEducationHistory.UpdateUserID = updateEducationHistory.UpdateUserID;
                updateEducationHistory.UpdateDate = updateEducationHistory.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateEducationHistory;
            }
            return educationHistory;
        }

        public async Task<bool> DeleteEducationHistoryAsync(int id)
        {
            var deleteEducationHistory = await dbContext.EducationHistory.FirstOrDefaultAsync(x => x.ID == id);

            if (deleteEducationHistory is not null)
            {
                dbContext.EducationHistory.Remove(deleteEducationHistory);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<EducationHistory> EducationHistoryGetDataAsync(int id)
        {
            return await dbContext.EducationHistory.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<EducationHistory>> EducationHistoryGetAllDataAsync()
        {
            return await dbContext.EducationHistory.ToListAsync();
        }

        public async Task<List<EducationHistory>> GetEducationHistoryByName(string name)
        {
            return await dbContext.EducationHistory.Where(x => x.Inistute == name).ToListAsync();
        }
    }
}
