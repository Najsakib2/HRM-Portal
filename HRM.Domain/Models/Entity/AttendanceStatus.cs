
namespace HRM.Domain
{
    public class AttendanceStatus
    {
        public int ID { get; set; }
        public required string Present { get; set; }
        public required string Absent { get; set; }
        public required string Leave { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
