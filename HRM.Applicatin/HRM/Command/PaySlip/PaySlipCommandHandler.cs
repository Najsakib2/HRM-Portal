using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddPaySlipCommandHandler : IRequestHandler<AddPaySlipCommand, PaySlip>
    {
        private readonly IPaySlipRepository _paySlipRepository;

        public AddPaySlipCommandHandler(IPaySlipRepository paySlipRepository)
        {
            _paySlipRepository = paySlipRepository;
        }

        public async Task<PaySlip> Handle(AddPaySlipCommand command, CancellationToken cancellationToken)
        {

            var addedPaySlip = await _paySlipRepository.AddPaySlipAsync(command.paySlip);
            return addedPaySlip;
        }
    }

    public class UpdatePaySlipCommandHandler : IRequestHandler<UpdatePaySlipCommand, PaySlip>
    {
        private readonly IPaySlipRepository _paySlipRepository;

        public UpdatePaySlipCommandHandler(IPaySlipRepository paySlipRepository)
        {
            _paySlipRepository = paySlipRepository;
        }

        public async Task<PaySlip> Handle(UpdatePaySlipCommand command, CancellationToken cancellationToken)
        {
            return await _paySlipRepository.UpdatePaySlipAsync(command.paySlipId, command.paySlip);
        }
    }

    public class DeletePaySlipCommandHandler : IRequestHandler<DeletePaySlipCommand, bool>
    {
        private readonly IPaySlipRepository _paySlipRepository;

        public DeletePaySlipCommandHandler(IPaySlipRepository paySlipRepository)
        {
            _paySlipRepository = paySlipRepository;
        }
        public async Task<bool> Handle(DeletePaySlipCommand command, CancellationToken cancellationToken)
        {
            return await _paySlipRepository.DeletePaySlipAsync(command.paySlipId);
        }
    }
}
