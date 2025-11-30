using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record EmployeeAttendanceGetDataQuery(int id) : IRequest<EmployeeAttendance>;
    public record EmployeeAttendanceGetAllDataQuery() : IRequest<IEnumerable<EmployeeAttendance>>;

}
