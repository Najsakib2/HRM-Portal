
using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class BankInformation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Users? User { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required string BankName { get; set; }
        public required string BranceName { get; set; }
        public int AccountNumber { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
