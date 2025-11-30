using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class EducationHistoryGetDataQueryHandler : IRequestHandler<EducationHistoryGetDataQuery, EducationHistory>
    {
        public readonly IEducationHistoryRepository _educationHistoryRepository;

        public EducationHistoryGetDataQueryHandler(IEducationHistoryRepository educationHistoryRepository)
        {
            _educationHistoryRepository = educationHistoryRepository;
        }
        public async Task<EducationHistory> Handle(EducationHistoryGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _educationHistoryRepository.EducationHistoryGetDataAsync(query.id);
        }
    }

    public class EducationHistoryGetAllDataQueryHandler : IRequestHandler<EducationHistoryGetAllDataQuery, IEnumerable<EducationHistory>>
    {
        public readonly IEducationHistoryRepository _educationHistoryRepository;

        public EducationHistoryGetAllDataQueryHandler(IEducationHistoryRepository educationHistoryRepository)
        {
            _educationHistoryRepository = educationHistoryRepository;
        }

        public async Task<IEnumerable<EducationHistory>> Handle(EducationHistoryGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _educationHistoryRepository.EducationHistoryGetAllDataAsync();
        }
    }
}
