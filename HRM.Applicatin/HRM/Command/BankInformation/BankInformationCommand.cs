using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddBankInformationCommand(BankInformation bankInformation) : IRequest<BankInformation>;
    public record UpdateBankInformationCommand(int bankInformationId, BankInformation bankInformation) : IRequest<BankInformation>;
    public record DeleteBankInformationCommand(int bankInformationId) : IRequest<bool>;
}

