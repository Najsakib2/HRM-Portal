using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record LeaveTypeGetDataQuery(int id) : IRequest<LeaveType>;
    public record LeaveTypeGetAllDataQuery() : IRequest<IEnumerable<LeaveType>>;
}
