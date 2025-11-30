using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddStatusCommand(Status status) : IRequest<Status>;
    public record UpdateStatusCommand(int statusId, Status status) : IRequest<Status>;
    public record DeleteStatusCommand(int statusId) : IRequest<bool>;
}

