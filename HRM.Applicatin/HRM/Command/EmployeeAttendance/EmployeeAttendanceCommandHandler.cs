using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddEmployeeAttendanceCommandHandler : IRequestHandler<AddEmployeeAttendanceCommand, EmployeeAttendance>
    {
        private readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;

        public AddEmployeeAttendanceCommandHandler(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _employeeAttendanceRepository = employeeAttendanceRepository;
        }

        public async Task<EmployeeAttendance> Handle(AddEmployeeAttendanceCommand command, CancellationToken cancellationToken)
        {

            var addedEmployeeAttendance = await _employeeAttendanceRepository.AddEmployeeAttendanceAsync(command.employeeAttendance);
            return addedEmployeeAttendance;
        }
    }

    public class UpdateEmployeeAttendanceCommandHandler : IRequestHandler<UpdateEmployeeAttendanceCommand, EmployeeAttendance>
    {
        private readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;

        public UpdateEmployeeAttendanceCommandHandler(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _employeeAttendanceRepository = employeeAttendanceRepository;
        }

        public async Task<EmployeeAttendance> Handle(UpdateEmployeeAttendanceCommand command, CancellationToken cancellationToken)
        {
            return await _employeeAttendanceRepository.UpdateEmployeeAttendanceAsync(command.employeeAttendanceId, command.employeeAttendance);
        }
    }

    public class DeleteEmployeeAttendanceCommandHandler : IRequestHandler<DeleteEmployeeAttendanceCommand, bool>
    {
        private readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;

        public DeleteEmployeeAttendanceCommandHandler(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _employeeAttendanceRepository = employeeAttendanceRepository;
        }
        public async Task<bool> Handle(DeleteEmployeeAttendanceCommand command, CancellationToken cancellationToken)
        {
            return await _employeeAttendanceRepository.DeleteEmployeeAttendanceAsync(command.employeeAttendanceId);
        }
    }
}
