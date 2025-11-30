using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public record AddEmployeeAttendanceCommand(EmployeeAttendance employeeAttendance) : IRequest<EmployeeAttendance>;
    public record UpdateEmployeeAttendanceCommand(int employeeAttendanceId, EmployeeAttendance employeeAttendance) : IRequest<EmployeeAttendance>;
    public record DeleteEmployeeAttendanceCommand(int employeeAttendanceId) : IRequest<bool>;
}
