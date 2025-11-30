using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AttendanceStatusGetDataQuery(int id) : IRequest<AttendanceStatus>;
    public record AttendanceStatusGetAllDataQuery() : IRequest<IEnumerable<AttendanceStatus>>;
    public record AttendanceStatusDuplicateCheckQuery(string checkData) : IRequest<ErrorOr<bool>>;
}

