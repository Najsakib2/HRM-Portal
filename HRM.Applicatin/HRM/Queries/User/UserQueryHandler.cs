using AutoMapper;
using ErrorOr;
using HRM.Applicatin.Common.Exceptions;
using HRM.Applicatin.Service;
using HRM.Application;
using HRM.Domain.Common.Errors;
using MediatR;
namespace HRM.Applicatin
{
    public class UserGetDataQueryHandler : IRequestHandler<UserGetDataQuery,ErrorOr<QueryUserDto>>
    {
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;

        public UserGetDataQueryHandler(IUserRepository userRepository,IMapper mapper)  
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<QueryUserDto>> Handle(UserGetDataQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.UserGetDataAsync(query.id);
            if (user == null) 
            {
                throw new NotFoundException("User not found");
            }

            var dto = _mapper.Map<QueryUserDto>(user);
            return dto;
        }
    }

    public class UserGetAllDataQueryHandler : IRequestHandler<UserGetAllDataQuery, ErrorOr<IEnumerable<QueryUserDto>>>
    {
        public readonly IUserRepository _userRepository;
        public readonly IRedisCacheService _redisCacheService;
        public readonly IMapper _mapper;

        public UserGetAllDataQueryHandler(IUserRepository userRepository,IRedisCacheService redisCacheService,IMapper mapper)
        {
            _userRepository= userRepository;
            _redisCacheService= redisCacheService;
            _mapper= mapper;
        }

        public async Task<ErrorOr<IEnumerable<QueryUserDto>>> Handle(UserGetAllDataQuery query, CancellationToken cancellationToken)
        {

            var users = await _userRepository.UserGetAllDataAsync();

            var usersMapping = _mapper.Map<IEnumerable<QueryUserDto>>(users);

            if (!usersMapping.Any())
            {
                throw new NotFoundException("Users not found");
            }

            return ErrorOrFactory.From(usersMapping);
        }
    }

    public class UserGetDataByCompanyIdQueryHandler : IRequestHandler<UserGetDataByCompanyIdQuery, ErrorOr<IEnumerable<QueryUserDto>>>
    {
        public readonly IUserRepository _userRepository;
        public readonly IRedisCacheService _redisCacheService;
        public readonly IMapper _mapper;

        public UserGetDataByCompanyIdQueryHandler(IUserRepository userRepository, IRedisCacheService redisCacheService, IMapper mapper)
        {
            _userRepository = userRepository;
            _redisCacheService = redisCacheService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<QueryUserDto>>> Handle(UserGetDataByCompanyIdQuery query, CancellationToken cancellationToken)
        {

            string cacheKey = CacheKeyHelper<QueryUserDto>.GetAllByCompany(query.companyId);

            // Try to get from cache first

            var cachedUsers= await _redisCacheService.GetAsync<IEnumerable<QueryUserDto>>(cacheKey);
            if (cachedUsers != null && cachedUsers.Any())
            {
                return ErrorOrFactory.From(cachedUsers);
            }

            // Cache miss then call repository
            var usersFromDb = await _userRepository.UserGetDataByCompanyID(query.companyId);

            var usersMapping = _mapper.Map<IEnumerable<QueryUserDto>>(usersFromDb);
            if (usersFromDb == null || !usersFromDb.Any())
            {
                return Errors.Common.DataNotFound;
            }

            // Save to cache for next time
            await _redisCacheService.SetAsync(cacheKey, usersMapping, TimeSpan.FromMinutes(5));

            return ErrorOrFactory.From(usersMapping);
        }
    }
}
