using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IEmployeeAttendanceRepository
    {
        Task<EmployeeAttendance> AddEmployeeAttendanceAsync(EmployeeAttendance employeeAttendance);
        Task<EmployeeAttendance> UpdateEmployeeAttendanceAsync(int employeeAttendanceId, EmployeeAttendance employeeAttendance);
        Task<bool> DeleteEmployeeAttendanceAsync(int employeeAttendanceId);
        Task<EmployeeAttendance> EmployeeAttendanceGetDataAsync(int id);
        //Task<List<EmployeeAttendance>> GetEmployeeAttendanceByName(string name);
        Task<IEnumerable<EmployeeAttendance>> EmployeeAttendanceGetAllDataAsync();
    }
}
