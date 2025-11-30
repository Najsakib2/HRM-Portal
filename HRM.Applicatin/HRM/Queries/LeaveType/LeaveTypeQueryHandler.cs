using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class LeaveTypeGetDataQueryHandler : IRequestHandler<LeaveTypeGetDataQuery, LeaveType>
    {
        public readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeGetDataQueryHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<LeaveType> Handle(LeaveTypeGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.LeaveTypeGetDataAsync(query.id);
        }
    }

    public class LeaveTypeGetAllDataQueryHandler : IRequestHandler<LeaveTypeGetAllDataQuery, IEnumerable<LeaveType>>
    {
        public readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeGetAllDataQueryHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<IEnumerable<LeaveType>> Handle(LeaveTypeGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.LeaveTypeGetAllDataAsync();
        }
    }
}

