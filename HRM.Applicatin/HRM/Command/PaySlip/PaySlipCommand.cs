using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public record AddPaySlipCommand(PaySlip paySlip) : IRequest<PaySlip>;
    public record UpdatePaySlipCommand(int paySlipId, PaySlip paySlip) : IRequest<PaySlip>;
    public record DeletePaySlipCommand(int paySlipId) : IRequest<bool>;
}