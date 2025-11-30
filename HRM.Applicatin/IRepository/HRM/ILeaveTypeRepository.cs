using HRM.Domain;

namespace HRM.Applicatin
{
    public interface ILeaveTypeRepository
    {
        Task<LeaveType> AddLeaveTypeAsync(LeaveType leaveType);
        Task<LeaveType> UpdateLeaveTypeAsync(int leaveTypeId, LeaveType leaveType);
        Task<bool> DeleteLeaveTypeAsync(int leaveTypeId);
        Task<LeaveType> LeaveTypeGetDataAsync(int id);
        //Task<List<LeaveType>> GetLeaveTypeByName(string name);
        Task<IEnumerable<LeaveType>> LeaveTypeGetAllDataAsync();
    }
}
