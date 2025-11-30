using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record StatusGetDataQuery(int id) : IRequest<Status>;
    public record StatusGetAllDataQuery() : IRequest<IEnumerable<Status>>;
}

