using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record DesignationGetDataQuery(int id) : IRequest<ErrorOr<Designation>>;
    public record DesignationGetDataByCompanyIdQuery(int companyId) : IRequest<ErrorOr<IEnumerable<Designation>>>;
    public record DesignationGetAllDataQuery() : IRequest<ErrorOr<IEnumerable<Designation>>>;

}
