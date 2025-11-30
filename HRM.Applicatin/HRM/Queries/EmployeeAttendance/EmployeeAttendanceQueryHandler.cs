using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class EmployeeAttendanceGetDataQueryHandler : IRequestHandler<EmployeeAttendanceGetDataQuery, EmployeeAttendance>
    {
        public readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;

        public EmployeeAttendanceGetDataQueryHandler(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _employeeAttendanceRepository = employeeAttendanceRepository;
        }
        public async Task<EmployeeAttendance> Handle(EmployeeAttendanceGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _employeeAttendanceRepository.EmployeeAttendanceGetDataAsync(query.id);
        }
    }

    public class EmployeeAttendanceGetAllDataQueryHandler : IRequestHandler<EmployeeAttendanceGetAllDataQuery, IEnumerable<EmployeeAttendance>>
    {
        public readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;

        public EmployeeAttendanceGetAllDataQueryHandler(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _employeeAttendanceRepository = employeeAttendanceRepository;
        }

        public async Task<IEnumerable<EmployeeAttendance>> Handle(EmployeeAttendanceGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _employeeAttendanceRepository.EmployeeAttendanceGetAllDataAsync();
        }
    }
}

