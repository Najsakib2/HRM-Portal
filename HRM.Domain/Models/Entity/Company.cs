namespace HRM.Domain
{
    public class Company
    {
        public int ID { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public int Phone { get; set; }
        public required string Web { get; set; }
        public int Fax { get; set; }
        public required string Logo { get; set; }
        public required int EntryUserID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}