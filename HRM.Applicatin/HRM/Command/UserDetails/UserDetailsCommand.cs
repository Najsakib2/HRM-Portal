using ErrorOr;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddUserDetailsCommand(UserDetails userDetails) : IRequest<ErrorOr<UserDetails>>;
    public record UpdateUserDetailsCommand(int userDetailsId, UserDetails userDetails) : IRequest<ErrorOr<UserDetails>>;
    public record DeleteUserDetailsCommand(int userDetailsId) : IRequest<ErrorOr<bool>>;
}
