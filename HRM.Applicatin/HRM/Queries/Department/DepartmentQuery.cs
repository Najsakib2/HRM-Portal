using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record DepartmentGetDataQuery(int id) : IRequest<ErrorOr<Department>>;
    public record DepartmentGetDataByCompanyIdQuery(int companyId) : IRequest<ErrorOr<IEnumerable<Department>>>;
    public record DepartmentGetAllDataQuery() : IRequest<ErrorOr<IEnumerable<Department>>>;
}
