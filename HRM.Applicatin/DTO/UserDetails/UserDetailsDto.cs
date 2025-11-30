namespace HRM.Application
{
    public class UserDetailsDto
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; }
        public string NIDNumber { get; set; }
        public string Gender { get; set; }
        public int? DepartmentID { get; set; }
        public int? DesignationID { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
        public string? Signature { get; set; }

    }
}
