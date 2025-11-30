using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        public async Task<Department> AddDepartmentAsync(Department department)
        {
            dbContext.Department.Add(department);
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            //var updateDepartment = await dbContext.Department.FirstOrDefaultAsync(x => x.Id == id);
            //if (updateDepartment is not null)
            //{
            //    updateDepartment.DepartmentCode = department.DepartmentCode;
            //    updateDepartment.DepartmentName = department.DepartmentName;
            //    updateDepartment.IsActive = department.IsActive;
            //    updateDepartment.EntryUseID = department.EntryUseID;
            //    updateDepartment.EntryDate = department.EntryDate;
            //    updateDepartment.UpdateUserID = department.UpdateUserID;
            //    updateDepartment.UpdateDate = department.UpdateDate;
            //    await dbContext.SaveChangesAsync();
            //    return updateDepartment;
            //}
            //return department;
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteDepartmentAsync(Department department)
        {
            dbContext.Department.Remove(department);
            return await dbContext.SaveChangesAsync() > 0;

        }

        public async Task<Department> DepartmentGetDataAsync(int id)
        {
            return await dbContext.Department.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Department>> DepartmentGetDataByCompanyIdAsync(int companyId)
        {
            return await dbContext.Department
                .Where(x => x.CompanyID == companyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> DepartmentGetAllDataAsync()
        {
            return await dbContext.Department.ToListAsync();
        }

        public async Task<bool> IsDepartmentExistAsync(string departmentName, int companyId)
        {
            return await dbContext.Department
                .AnyAsync(x => x.DepartmentName == departmentName && x.CompanyID == companyId);
        }

        public async Task<bool> IsDepartmentInUseAsync(int departmentId)
        {
            if (await dbContext.UserDetails.AnyAsync(e => e.DepartmentID == departmentId))
                return true;

            if (await dbContext.Designation.AnyAsync(e => e.DepartmentID == departmentId))
                return true;

            return false;
        }
    }
}
