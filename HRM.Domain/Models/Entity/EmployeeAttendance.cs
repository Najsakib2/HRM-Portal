using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class EmployeeAttendance
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public int UserID { get; set; }
        public Users? User { get; set; }
        public int AttendanceStatusID { get; set; }
        public AttendanceStatus? AttendanceStatus { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }        
        public int Late { get; set; }
        public int Break { get; set; }
        public int ProductionHours { get; set; }
        public bool IsAdmin { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
