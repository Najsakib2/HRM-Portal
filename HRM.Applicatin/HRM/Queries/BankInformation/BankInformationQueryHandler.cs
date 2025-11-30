using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class BankInformationGetDataQueryHandler : IRequestHandler<BankInformationGetDataQuery, BankInformation>
    {
        public readonly IBankInformationRepository _bankInformationRepository;

        public BankInformationGetDataQueryHandler(IBankInformationRepository bankInformationRepository)
        {
            _bankInformationRepository = bankInformationRepository;
        }
        public async Task<BankInformation> Handle(BankInformationGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _bankInformationRepository.BankInformationGetDataAsync(query.id);
        }
    }

    public class BankInformationGetAllDataQueryHandler : IRequestHandler<BankInformationGetAllDataQuery, IEnumerable<BankInformation>>
    {
        public readonly IBankInformationRepository _bankInformationRepository;

        public BankInformationGetAllDataQueryHandler(IBankInformationRepository bankInformationRepository)
        {
            _bankInformationRepository = bankInformationRepository;
        }

        public async Task<IEnumerable<BankInformation>> Handle(BankInformationGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _bankInformationRepository.BankInformationGetAllDataAsync();
        }
    }
}
