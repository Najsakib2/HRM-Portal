using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddDepartmentCommand(Department department) : IRequest<ErrorOr<Department>>;
    public record UpdateDepartmentCommand(int departmentId, Department department) : IRequest<ErrorOr<Department>>;
    public record DeleteDepartmentCommand(int departmentId) : IRequest<ErrorOr<bool>>;
}
