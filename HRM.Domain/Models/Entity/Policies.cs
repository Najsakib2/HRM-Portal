using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class Policies
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public required string Description { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
