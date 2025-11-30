using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public class AddAttendanceStatusCommandHandler : IRequestHandler<AddAttendanceStatusCommand, AttendanceStatus>
    {
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public AddAttendanceStatusCommandHandler(IAttendanceStatusRepository attendanceStatusRepository)
        {
            _attendanceStatusRepository = attendanceStatusRepository;
        }

        public async Task<AttendanceStatus> Handle(AddAttendanceStatusCommand command, CancellationToken cancellationToken)
        {

            var addedAttendanceStatus = await _attendanceStatusRepository.AddAttendanceStatusAsync(command.attendanceStatus);
            return addedAttendanceStatus;
        }
    }

    public class UpdateAttendanceStatusCommandHandler : IRequestHandler<UpdateAttendanceStatusCommand, AttendanceStatus>
    {
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public UpdateAttendanceStatusCommandHandler(IAttendanceStatusRepository attendanceStatusRepository)
        {
            _attendanceStatusRepository = attendanceStatusRepository;
        }

        public async Task<AttendanceStatus> Handle(UpdateAttendanceStatusCommand command, CancellationToken cancellationToken)
        {
            return await _attendanceStatusRepository.UpdateAttendanceStatusAsync(command.attendanceStatusId, command.attendanceStatus);
        }
    }

    public class DeleteAttendanceStatusCommandHandler : IRequestHandler<DeleteAttendanceStatusCommand, bool>
    {
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public DeleteAttendanceStatusCommandHandler(IAttendanceStatusRepository attendanceStatusRepository)
        {
            _attendanceStatusRepository = attendanceStatusRepository;
        }
        public async Task<bool> Handle(DeleteAttendanceStatusCommand command, CancellationToken cancellationToken)
        {
            return await _attendanceStatusRepository.DeleteAttendanceStatusAsync(command.attendanceStatusId);
        }
    }
}