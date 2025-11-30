
namespace HRM.Domain
{
    public class LeaveType
    {
        public int ID { get; set; }
        public required string MedicalLeave { get; set; }
        public required string CasualLeave { get; set; }
        public required string AnnualLeave { get; set; }

        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
