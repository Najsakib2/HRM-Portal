using HRM.Domain;


namespace HRM.Applicatin
{
    public interface IDesignationRepository
    {
        Task<Designation> AddDesignationAsync(Designation designation);
        Task<Designation> UpdateDesignationAsync(int designationId, Designation designation);
        Task<bool> DeleteDesignationAsync(Designation designation);
        Task<List<Designation>> GetDesignationByName(string name);
        Task<Designation> DesignationGetDataAsync(int id);
        Task<IEnumerable<Designation>> DesignationGetDataByCompanyIdAsync(int companyId);
        Task<IEnumerable<Designation>> DesignationGetAllDataAsync();
        Task<bool> IsDesignationExistAsync(string designationName, int companyId);
        Task<bool> IsDesignationInUseAsync(int designationId);
    }
}
