using ErrorOr;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRM.Applicatin
{
    public record AddUserCommand(Users user,IFormFile image, IFormFile signature) : IRequest<ErrorOr<Users>>;
    public record UpdateUserCommand(Users user, IFormFile image, IFormFile signature) : IRequest<ErrorOr<Users>>;
    public record DeleteUserCommand(int userId) : IRequest<ErrorOr<bool>>;
}
