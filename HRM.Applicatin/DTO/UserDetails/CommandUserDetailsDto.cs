namespace HRM.Applicatin
{
    public class CommandUserDetailsDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public required int NIDNumber { get; set; }
        public int DepartmentID { get; set; }
        public int DesignationID { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
        public string? Signature { get; set; }
        public required int EntryUserID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
