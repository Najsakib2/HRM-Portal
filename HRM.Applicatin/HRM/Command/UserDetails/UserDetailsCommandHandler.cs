using AutoMapper;
using ErrorOr;
using HRM.Applicatin.Service;
using HRM.Application.IRepository.Authentication;
using HRM.Domain;
using MediatR;
using HRM.Domain.Common.Errors;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRM.Applicatin
{
    public class AddUserDetailsCommandHandler : IRequestHandler<AddUserDetailsCommand, ErrorOr<UserDetails>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ITokenUser _tokenUser;

        public AddUserDetailsCommandHandler(IUserDetailsRepository userDetailsRepository,IRedisCacheService redisCacheService, IMapper mapper, ITokenUser tokenUser)
        {
            _userDetailsRepository = userDetailsRepository;
            _cacheService = redisCacheService;
            _mapper = mapper;
            _tokenUser = tokenUser;
        }

        public async Task<ErrorOr<UserDetails>> Handle(AddUserDetailsCommand command, CancellationToken cancellationToken)
        {
            int companyId = int.Parse(_tokenUser.GetUserCompanyId());

            var addedUserDetails = await _userDetailsRepository.AddUserDetailsAsync(command.userDetails);

            string cacheKey = CacheKeyHelper<UserDetails>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return addedUserDetails;
        }
    }

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, ErrorOr<UserDetails>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly ITokenUser _tokenUser;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler(IUserDetailsRepository userDetailsRepository, IRedisCacheService cacheService, ITokenUser tokenUser, IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _cacheService = cacheService;
            _tokenUser = tokenUser;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserDetails>> Handle(UpdateUserDetailsCommand command, CancellationToken cancellationToken)
        {

            int companyId = int.Parse(_tokenUser.GetUserCompanyId());

            var existingUserDetails = await _userDetailsRepository.UserDetailsGetDataAsync(command.userDetails.Id);

            if (existingUserDetails is null)
                return Errors.Common.DataNotFound;

            // Map updated fields onto existing entity
            _mapper.Map(command.userDetails, existingUserDetails);

            string cacheKey = CacheKeyHelper<UserDetails>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return await _userDetailsRepository.UpdateUserDetailsAsync(command.userDetailsId, command.userDetails);
        }
    }

    public class DeleteUserDetailsCommandHandler : IRequestHandler<DeleteUserDetailsCommand, ErrorOr<bool>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly ITokenUser _tokenUser;

        public DeleteUserDetailsCommandHandler(IUserDetailsRepository userDetailsRepository,IRedisCacheService redisCacheService, ITokenUser tokenUser)
        {
            _userDetailsRepository = userDetailsRepository;
            _cacheService = redisCacheService;
            _tokenUser = tokenUser;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteUserDetailsCommand command, CancellationToken cancellationToken)
        {
            int companyId = int.Parse(_tokenUser.GetUserCompanyId());
            string cacheKey = CacheKeyHelper<UserDetails>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return await _userDetailsRepository.DeleteUserDetailsAsync(command.userDetailsId);
        }
    }
}
