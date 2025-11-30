using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record UserDetailsGetDataQuery(int id) : IRequest<ErrorOr<UserDetails>>;
    public record UserDetailsGetAllDataQuery() : IRequest<ErrorOr<IEnumerable<UserDetails>>>;

}
