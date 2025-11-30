using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class StatusGetDataQueryHandler : IRequestHandler<StatusGetDataQuery, Status>
    {
        public readonly IStatusRepository _statusRepository;

        public StatusGetDataQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<Status> Handle(StatusGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _statusRepository.StatusGetDataAsync(query.id);
        }
    }

    public class StatusGetAllDataQueryHandler : IRequestHandler<StatusGetAllDataQuery, IEnumerable<Status>>
    {
        public readonly IStatusRepository _statusRepository;

        public StatusGetAllDataQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> Handle(StatusGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _statusRepository.StatusGetAllDataAsync();
        }
    }
}

