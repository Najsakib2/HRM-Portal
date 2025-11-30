using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddAttendanceStatusCommand(AttendanceStatus attendanceStatus) : IRequest<AttendanceStatus>;
    public record UpdateAttendanceStatusCommand(int attendanceStatusId, AttendanceStatus attendanceStatus) : IRequest<AttendanceStatus>;
    public record DeleteAttendanceStatusCommand(int attendanceStatusId) : IRequest<bool>;
}
