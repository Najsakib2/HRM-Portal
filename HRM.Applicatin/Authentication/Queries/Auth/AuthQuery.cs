using HRM.Application;
using MediatR;

namespace HRM.Applicatin
{
    public record SignInQuery(string email, string password) : IRequest<QueryUserDto>;
}
