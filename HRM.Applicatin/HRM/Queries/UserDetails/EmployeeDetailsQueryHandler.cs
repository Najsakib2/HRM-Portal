using ErrorOr;
using HRM.Applicatin.Common.Exceptions;
using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public class UserDetailsGetDataQueryHandler : IRequestHandler<UserDetailsGetDataQuery, ErrorOr<UserDetails>>
    {
        public readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsGetDataQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }
        public async Task<ErrorOr<UserDetails>> Handle(UserDetailsGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _userDetailsRepository.UserDetailsGetDataAsync(query.id);
        }
    }
    public class UserDetailsGetAllDataQueryHandler : IRequestHandler<UserDetailsGetAllDataQuery, ErrorOr<IEnumerable<UserDetails>>>
    {
        public readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsGetAllDataQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<ErrorOr<IEnumerable<UserDetails>>> Handle(UserDetailsGetAllDataQuery query, CancellationToken cancellationToken)
        {
            var userDetails = await _userDetailsRepository.UserDetailsGetAllDataAsync();

            if (!userDetails.Any())
            {
                throw new NotFoundException("UserDetails not found");
            }

            return ErrorOrFactory.From(userDetails);
        }
    }
}
