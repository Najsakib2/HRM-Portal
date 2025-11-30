
using System.Text.Json.Serialization;

namespace HRM.Domain
{
    public class Users
    {
        public int ID { get; set; }
        public required int CompanyID { get; set; }
        public Company? Company { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public required DateTime JoinDate { get; set; }
        public required int EntryUserID { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UserDetails userDetails { get; set; }
    }
}
