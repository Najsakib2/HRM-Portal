using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(Department department);
        Task<Department> DepartmentGetDataAsync(int id);
        Task<IEnumerable<Department>> DepartmentGetDataByCompanyIdAsync(int companyId);
        Task<IEnumerable<Department>> DepartmentGetAllDataAsync();
        Task<bool> IsDepartmentExistAsync(string departmentName, int companyId);
        Task<bool> IsDepartmentInUseAsync(int departmentId);
    }
}
