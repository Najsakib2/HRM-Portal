using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddBankInformationCommandHandler : IRequestHandler<AddBankInformationCommand, BankInformation>
    {
        private readonly IBankInformationRepository _bankInformationRepository;

        public AddBankInformationCommandHandler(IBankInformationRepository bankInformationRepository)
        {
            _bankInformationRepository = bankInformationRepository;
        }

        public async Task<BankInformation> Handle(AddBankInformationCommand command, CancellationToken cancellationToken)
        {

            var addedBankInformation = await _bankInformationRepository.AddBankInformationAsync(command.bankInformation);
            return addedBankInformation;
        }
    }

    public class UpdateBankInformationCommandHandler : IRequestHandler<UpdateBankInformationCommand, BankInformation>
    {
        private readonly IBankInformationRepository _bankInformationRepository;

        public UpdateBankInformationCommandHandler(IBankInformationRepository bankInformationRepository)
        {
            _bankInformationRepository = bankInformationRepository;
        }

        public async Task<BankInformation> Handle(UpdateBankInformationCommand command, CancellationToken cancellationToken)
        {
            return await _bankInformationRepository.UpdateBankInformationAsync(command.bankInformationId, command.bankInformation);
        }
    }

    public class DeleteBankInformationCommandHandler : IRequestHandler<DeleteBankInformationCommand, bool>
    {
        private readonly IBankInformationRepository _bankInformationRepository;

        public DeleteBankInformationCommandHandler(IBankInformationRepository bankInformationRepository)
        {
            _bankInformationRepository = bankInformationRepository;
        }
        public async Task<bool> Handle(DeleteBankInformationCommand command, CancellationToken cancellationToken)
        {
            return await _bankInformationRepository.DeleteBankInformationAsync(command.bankInformationId);
        }
    }
}
