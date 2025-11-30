using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class PaySlip
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required int UserID { get; set; }
        public Users? User { get; set; }
        public int BasicSalary { get; set; }
        public int HousingAllowance { get; set; }
        public int TransportAllowance { get; set; }
        public int OtherAllowances { get; set; }
        public int TotalSalary { get; set; }
        public bool IsActive { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}