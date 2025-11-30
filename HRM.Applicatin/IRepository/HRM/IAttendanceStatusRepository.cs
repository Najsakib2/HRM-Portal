using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IAttendanceStatusRepository
    {
        Task<AttendanceStatus> AddAttendanceStatusAsync(AttendanceStatus attendanceStatus);
        Task<AttendanceStatus> UpdateAttendanceStatusAsync(int attendanceStatusId, AttendanceStatus attendanceStatus);
        Task<bool> DeleteAttendanceStatusAsync(int AttendanceStatusId);
        Task<AttendanceStatus> AttendanceStatusGetDataAsync(int id);
        //Task<List<AttendanceStatus>> GetAttendanceStatusByName(string name);
        Task<IEnumerable<AttendanceStatus>> AttendanceStatusGetAllDataAsync();
    }
}
