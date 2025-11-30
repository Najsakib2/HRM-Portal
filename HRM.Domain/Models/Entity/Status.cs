
namespace HRM.Domain
{
    public class Status
    {
        public int Id { get; set; }
        public required string Approved { get; set; }
        public required string Reject { get; set; }
        public required string Pending { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}