using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class Leave
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public int UserID { get; set; }
        public Users? User { get; set; }
        public int LeaveTypeID { get; set; }
        public LeaveType? LeaveType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public required string Reason { get; set; }
        public Status? Status { get; set; }
        public int StatusID { get; set; }
        public int AnnualLeave { get; set; }
        public int CasualLeave { get; set; }
        public int MedicalLeave { get; set; }
        public int RemainingLeave { get; set; }
        public DateTime ApprovalDate { get; set; }
        public required string ApprovalComments { get; set; }
        public bool IsAdmin { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
