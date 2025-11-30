
namespace HRM.Domain
{
    public class Holidays
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required string Title { get; set; }
        public DateTime Date { get; set; }
        public required string HolidayName { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
