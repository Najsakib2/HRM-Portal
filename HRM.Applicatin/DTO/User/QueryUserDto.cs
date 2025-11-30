
namespace HRM.Application
{
    public class QueryUserDto
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserDetailsDto? UserDetails { get; set; }
    }
}
