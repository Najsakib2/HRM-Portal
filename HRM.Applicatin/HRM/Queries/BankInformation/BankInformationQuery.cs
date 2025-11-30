using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record BankInformationGetDataQuery(int id) : IRequest<BankInformation>;
    public record BankInformationGetAllDataQuery() : IRequest<IEnumerable<BankInformation>>;
}
