using ErrorOr;
using HRM.Applicatin.Common.Exceptions;
using HRM.Applicatin.Service;
using HRM.Domain;
using MediatR;
using HRM.Domain.Common.Errors;
namespace HRM.Applicatin
{
    public class DesignationGetDataQueryHandler : IRequestHandler<DesignationGetDataQuery, ErrorOr<Designation>>
    {
        public readonly IDesignationRepository _designationRepository;

        public DesignationGetDataQueryHandler(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }
        public async Task<ErrorOr<Designation>> Handle(DesignationGetDataQuery query, CancellationToken cancellationToken)
        {

            var designation = await _designationRepository.DesignationGetDataAsync(query.id);
            if (designation == null)
            {
                return Errors.Common.DataNotFound;
            }

            return designation;
        }
    }

    public class DesignationGetDataByCompanyIdQueryHandler : IRequestHandler<DesignationGetDataByCompanyIdQuery, ErrorOr<IEnumerable<Designation>>>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IRedisCacheService _cacheService;

        public DesignationGetDataByCompanyIdQueryHandler(IDesignationRepository designationRepository, IRedisCacheService redisCacheService)
        {
            _designationRepository = designationRepository;
            _cacheService = redisCacheService;
        }

        public async Task<ErrorOr<IEnumerable<Designation>>> Handle(DesignationGetDataByCompanyIdQuery query, CancellationToken cancellationToken)
        {
            string cacheKey = CacheKeyHelper<Designation>.GetAllByCompany(query.companyId);

            // Try to get from cache first
            var cachedDesignations = await _cacheService.GetAsync<IEnumerable<Designation>>(cacheKey);
            if (cachedDesignations != null && cachedDesignations.Any())
            {
                return ErrorOrFactory.From(cachedDesignations);
            }

            // Cache miss then call repository
            var designationsFromDb = await _designationRepository.DesignationGetDataByCompanyIdAsync(query.companyId);
            if (designationsFromDb == null)
            {
                return Errors.Common.DataNotFound;
            }

            // Save to cache for next time
            await _cacheService.SetAsync(cacheKey, designationsFromDb, TimeSpan.FromMinutes(5));

            return ErrorOrFactory.From(designationsFromDb);
        }
    }
    public class DesignationGetAllDataQueryHandler : IRequestHandler<DesignationGetAllDataQuery, ErrorOr<IEnumerable<Designation>>>
    {
        public readonly IDesignationRepository _designationRepository;

        public DesignationGetAllDataQueryHandler(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<ErrorOr<IEnumerable<Designation>>> Handle(DesignationGetAllDataQuery query, CancellationToken cancellationToken)
        {
            var designations = await _designationRepository.DesignationGetAllDataAsync();

            if (designations == null)
            {
                return Errors.Common.DataNotFound;
            }
            return ErrorOrFactory.From(designations);
        }
    }
}

