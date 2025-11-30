using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record EducationHistoryGetDataQuery(int id) : IRequest<EducationHistory>;
    public record EducationHistoryGetAllDataQuery() : IRequest<IEnumerable<EducationHistory>>;
}
