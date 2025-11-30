
using ErrorOr;
using HRM.Applicatin.Service;
using HRM.Application.IRepository.Authentication;
using HRM.Domain;
using HRM.Domain.Common.Errors;
using MediatR;
namespace HRM.Applicatin
{
    public class DepartmentGetDataQueryHandler : IRequestHandler<DepartmentGetDataQuery, ErrorOr<Department>>
    {
        public readonly IDepartmentRepository _departmentRepository;

        public DepartmentGetDataQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ErrorOr<Department>> Handle(DepartmentGetDataQuery query, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.DepartmentGetDataAsync(query.id);
            if (departments == null) 
            {
                return Errors.Common.DataNotFound;
            }

            return departments;
        }
    }

    public class DepartmentGetDataByCompanyIdQueryHandler:IRequestHandler<DepartmentGetDataByCompanyIdQuery, ErrorOr<IEnumerable<Department>>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRedisCacheService _cacheService;

        public DepartmentGetDataByCompanyIdQueryHandler(IDepartmentRepository departmentRepository,IRedisCacheService cacheService)
        {
            _departmentRepository = departmentRepository;
            _cacheService = cacheService;
        }

        public async Task<ErrorOr<IEnumerable<Department>>> Handle(DepartmentGetDataByCompanyIdQuery query,CancellationToken cancellationToken)
        {
            string cacheKey = CacheKeyHelper<Department>.GetAllByCompany(query.companyId);

            // Try to get from cache first
            var cachedDepartments = await _cacheService.GetAsync<IEnumerable<Department>>(cacheKey);
            if (cachedDepartments != null && cachedDepartments.Any())
            {
                return ErrorOrFactory.From(cachedDepartments);
            }

            // Cache miss then call repository
            var departmentsFromDb = await _departmentRepository.DepartmentGetDataByCompanyIdAsync(query.companyId);
            if (departmentsFromDb == null || !departmentsFromDb.Any())
            {
                return Errors.Common.DataNotFound;
            }

            // Save to cache for next time
            await _cacheService.SetAsync(cacheKey, departmentsFromDb, TimeSpan.FromMinutes(5));

            return ErrorOrFactory.From(departmentsFromDb); 
        }
    }


    public class DepartmentGetAllDataQueryHandler : IRequestHandler<DepartmentGetAllDataQuery, ErrorOr<IEnumerable<Department>>>
        {
        public readonly IDepartmentRepository _departmentRepository;

        public DepartmentGetAllDataQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ErrorOr<IEnumerable<Department>>> Handle(DepartmentGetAllDataQuery query, CancellationToken cancellationToken)
        {

            var departments = await _departmentRepository.DepartmentGetAllDataAsync();

            if (departments == null)
            {
                return Errors.Common.DataNotFound;
            }
            return ErrorOrFactory.From(departments);
        }
    }
}

