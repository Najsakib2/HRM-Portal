using ErrorOr;
using HRM.Application;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record UserGetDataQuery(int id) : IRequest<ErrorOr<QueryUserDto>>;
    public record UserGetAllDataQuery() : IRequest<ErrorOr<IEnumerable<QueryUserDto>>>;
    public record UserGetDataByCompanyIdQuery(int companyId) : IRequest<ErrorOr<IEnumerable<QueryUserDto>>>;
    public record UserDuplicateCheckQuery(string checkData) : IRequest<ErrorOr<bool>>;
}
