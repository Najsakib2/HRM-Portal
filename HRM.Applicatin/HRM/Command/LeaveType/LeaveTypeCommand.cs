using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public record AddLeaveTypeCommand(LeaveType leaveType) : IRequest<LeaveType>;
    public record UpdateLeaveTypeCommand(int leaveTypeId, LeaveType leaveType) : IRequest<LeaveType>;
    public record DeleteLeaveTypeCommand(int leaveTypeId) : IRequest<bool>;
}
