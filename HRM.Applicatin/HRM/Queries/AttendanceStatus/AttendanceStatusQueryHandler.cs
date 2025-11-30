using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class AttendanceStatusGetDataQueryHandler : IRequestHandler<AttendanceStatusGetDataQuery, AttendanceStatus>
    {
        public readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public AttendanceStatusGetDataQueryHandler(IAttendanceStatusRepository attendanceStatusRepository)
        {
            _attendanceStatusRepository = attendanceStatusRepository;
        }
        public async Task<AttendanceStatus> Handle(AttendanceStatusGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _attendanceStatusRepository.AttendanceStatusGetDataAsync(query.id);
        }
    }

    public class AttendanceStatusGetAllDataQueryHandler : IRequestHandler<AttendanceStatusGetAllDataQuery, IEnumerable<AttendanceStatus>>
    {
        public readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public AttendanceStatusGetAllDataQueryHandler(IAttendanceStatusRepository attendanceStatusRepository)
        {
            _attendanceStatusRepository = attendanceStatusRepository;
        }

        public async Task<IEnumerable<AttendanceStatus>> Handle(AttendanceStatusGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _attendanceStatusRepository.AttendanceStatusGetAllDataAsync();
        }
    }
}
