using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record PaySlipGetDataQuery(int id) : IRequest<PaySlip>;
    public record PaySlipGetAllDataQuery() : IRequest<IEnumerable<PaySlip>>;
}
