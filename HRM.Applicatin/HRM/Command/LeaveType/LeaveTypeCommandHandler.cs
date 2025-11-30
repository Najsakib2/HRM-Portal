using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddLeaveTypeCommandHandler : IRequestHandler<AddLeaveTypeCommand, LeaveType>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public AddLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveType> Handle(AddLeaveTypeCommand command, CancellationToken cancellationToken)
        {

            var addedLeaveType = await _leaveTypeRepository.AddLeaveTypeAsync(command.leaveType);
            return addedLeaveType;
        }
    }

    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, LeaveType>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveType> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.UpdateLeaveTypeAsync(command.leaveTypeId, command.leaveType);
        }
    }

    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, bool>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<bool> Handle(DeleteLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.DeleteLeaveTypeAsync(command.leaveTypeId);
        }
    }
}
