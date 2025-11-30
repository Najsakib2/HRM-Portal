using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record HolidaysGetDataQuery(int id) : IRequest<Holidays>;
    public record HolidaysGetAllDataQuery() : IRequest<IEnumerable<Holidays>>;
}
