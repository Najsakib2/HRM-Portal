using AutoMapper;
using ErrorOr;
using HRM.Applicatin.Service;
using HRM.Application.IRepository.Authentication;
using HRM.Domain;
using HRM.Domain.Common.Errors;
using MediatR;

namespace HRM.Applicatin
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, ErrorOr<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRedisCacheService _cacheService;

        public AddDepartmentCommandHandler(IDepartmentRepository departmentRepository, IRedisCacheService cacheService)
        {
            _departmentRepository = departmentRepository;
            _cacheService = cacheService;
        }

        public async Task<ErrorOr<Department>> Handle(AddDepartmentCommand command, CancellationToken cancellationToken)
        {

            bool isExist = await _departmentRepository.IsDepartmentExistAsync(command.department.DepartmentName, command.department.CompanyID);
            if (isExist)
            {
               return Errors.Common.Exist;
            }

            var addedDepartment = await _departmentRepository.AddDepartmentAsync(command.department);

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Department>.GetAllByCompany(command.department.CompanyID);
            await _cacheService.RemoveAsync(cacheKey);

            return addedDepartment;
        }
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ErrorOr<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IRedisCacheService redisCache,IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _cacheService = redisCache;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Department>> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
        {

            var existingDepartment = await _departmentRepository.DepartmentGetDataAsync(command.departmentId);

            if (existingDepartment is null)
                return Errors.Common.DataNotFound;

            // Map updated fields onto existing entity
            _mapper.Map(command.department, existingDepartment);

            var department = await _departmentRepository.UpdateDepartmentAsync(command.department);

            // Invalidate cache
            string cacheKey = CacheKeyHelper<Department>.GetAllByCompany(command.department.CompanyID);
            await _cacheService.RemoveAsync(cacheKey);
            return department;
        }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ErrorOr<bool>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly ITokenUser _tokenUser;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IRedisCacheService cacheService,ITokenUser tokenUser)
        {
            _departmentRepository = departmentRepository;
            _cacheService = cacheService;
            _tokenUser = tokenUser;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
        {

            int companyId = int.Parse(_tokenUser.GetUserCompanyId());

            var department = await _departmentRepository.DepartmentGetDataAsync(command.departmentId);

            if (department == null)
            {
                return Errors.Common.DataNotFound;
            }

            bool isInUse = await _departmentRepository.IsDepartmentInUseAsync(command.departmentId);
            if (isInUse) 
            {
                return Errors.Common.IsUsed("Department");
            }

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Department>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return await _departmentRepository.DeleteDepartmentAsync(department);
        }
    }
}
