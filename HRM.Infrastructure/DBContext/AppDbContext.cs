using HRM.Domain;
using HRM.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Company> Company{ get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Policies> Policies { get; set; }
        public DbSet<EducationHistory> EducationHistory { get; set; }
        public DbSet<AttendanceStatus> AttendanceStatus { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<BankInformation> BankInformation { get; set; }
        public DbSet<PaySlip> PaySlip { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
