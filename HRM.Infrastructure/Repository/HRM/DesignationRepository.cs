using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class DesignationRepository(AppDbContext dbContext) : IDesignationRepository
    {

        public async Task<Designation> AddDesignationAsync(Designation designation)
        {
            try
            {
                dbContext.Designation.Add(designation);
                await dbContext.SaveChangesAsync();
                return designation;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle DB update specific errors (FK, unique constraint, etc.)
                throw new Exception("An error occurred while saving the designation to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Handle general errors
                throw new Exception("An unexpected error occurred while adding the designation.", ex);
            }
        }

        public async Task<Designation> UpdateDesignationAsync(int id, Designation designation)
        {
            await dbContext.SaveChangesAsync();
            return designation;
        }

        public async Task<bool> DeleteDesignationAsync(Designation designation)
        {
            dbContext.Designation.Remove(designation);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Designation> DesignationGetDataAsync(int id)
        {
            return await dbContext.Designation.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Designation>> DesignationGetDataByCompanyIdAsync(int companyId)
        {
            return await dbContext.Designation
                .Where(x => x.CompanyID == companyId).Include(x => x.Department).ToListAsync(); // Include related Department
        }

        public async Task<IEnumerable<Designation>> DesignationGetAllDataAsync()
        {
            return await dbContext.Designation.ToListAsync();
        }

        public async Task<List<Designation>> GetDesignationByName(string name)
        {
            return await dbContext.Designation.Where(x => x.DesignationName == name).ToListAsync();
        }

        public async Task<bool> IsDesignationExistAsync(string designationName, int companyId)
        {
            return await dbContext.Designation
              .AnyAsync(x => x.DesignationName == designationName && x.CompanyID == companyId);
        }

        public async Task<bool> IsDesignationInUseAsync(int designationId)
        {
            if (await dbContext.UserDetails.AnyAsync(e => e.DesignationID == designationId))
                return true;

            return false;
        }
    }
}
