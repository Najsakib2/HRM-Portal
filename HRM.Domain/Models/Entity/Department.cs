
namespace HRM.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required string DepartmentCode { get; set; }
        public required string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public required int EntryUseID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
