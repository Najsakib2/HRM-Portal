using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddDesignationCommand(Designation designation) : IRequest<ErrorOr<Designation>>;
    public record UpdateDesignationCommand(int designationId, Designation designation) : IRequest<ErrorOr<Designation>>;
    public record DeleteDesignationCommand(int designationId) : IRequest<ErrorOr<bool>>;
}
