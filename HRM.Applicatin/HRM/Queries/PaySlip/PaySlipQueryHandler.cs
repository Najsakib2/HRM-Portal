using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class PaySlipGetDataQueryHandler : IRequestHandler<PaySlipGetDataQuery, PaySlip>
    {
        public readonly IPaySlipRepository _paySlipRepository;

        public PaySlipGetDataQueryHandler(IPaySlipRepository paySlipRepository)
        {
            _paySlipRepository = paySlipRepository;
        }
        public async Task<PaySlip> Handle(PaySlipGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _paySlipRepository.PaySlipGetDataAsync(query.id);
        }
    }

    public class PaySlipGetAllDataQueryHandler : IRequestHandler<PaySlipGetAllDataQuery, IEnumerable<PaySlip>>
    {
        public readonly IPaySlipRepository _paySlipRepository;

        public PaySlipGetAllDataQueryHandler(IPaySlipRepository paySlipRepository)
        {
            _paySlipRepository = paySlipRepository;
        }

        public async Task<IEnumerable<PaySlip>> Handle(PaySlipGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _paySlipRepository.PaySlipGetAllDataAsync();
        }
    }
}

