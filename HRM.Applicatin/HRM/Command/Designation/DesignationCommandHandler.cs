using ErrorOr;
using HRM.Applicatin.Service;
using HRM.Domain;
using MediatR;
using HRM.Domain.Common.Errors;
using AutoMapper;
using System.ComponentModel.Design;
using HRM.Application.IRepository.Authentication;


namespace HRM.Applicatin
{
    public class AddDesignationCommandHandler : IRequestHandler<AddDesignationCommand, ErrorOr<Designation>>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IRedisCacheService _cacheService;


        public AddDesignationCommandHandler(IDesignationRepository designationRepository, IRedisCacheService cacheService)
        {
            _designationRepository = designationRepository;
            _cacheService = cacheService;
        }

        public async Task<ErrorOr<Designation>> Handle(AddDesignationCommand command, CancellationToken cancellationToken)
        {

            bool isExist = await _designationRepository.IsDesignationExistAsync(command.designation.DesignationName, command.designation.CompanyID);
            if (isExist)
            {
                return Errors.Common.Exist;
            }

            var addedDesignation = await _designationRepository.AddDesignationAsync(command.designation);

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Designation>.GetAllByCompany(command.designation.CompanyID);
            await _cacheService.RemoveAsync(cacheKey);

            return addedDesignation;
        }
    }

    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, ErrorOr<Designation>>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateDesignationCommandHandler(IDesignationRepository designationRepository, IRedisCacheService cacheService, IMapper mapper)
        {
            _designationRepository = designationRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Designation>> Handle(UpdateDesignationCommand command, CancellationToken cancellationToken)
        {

            var existingDesignation = await _designationRepository.DesignationGetDataAsync(command.designationId);

            if (existingDesignation is null)
                return Errors.Common.DataNotFound;

            // Map updated fields onto existing entity
            _mapper.Map(command.designation, existingDesignation);

            var upadeteddesignation = await _designationRepository.UpdateDesignationAsync(command.designationId, command.designation);

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Designation>.GetAllByCompany(command.designation.CompanyID);
            await _cacheService.RemoveAsync(cacheKey);

            return upadeteddesignation;
        }
    }

    public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand, ErrorOr<bool>>
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IRedisCacheService _cacheService;
        private readonly ITokenUser _tokenUser;

        public DeleteDesignationCommandHandler(IDesignationRepository designationRepository, IRedisCacheService cacheService, ITokenUser tokenUser)
        {
            _designationRepository = designationRepository;
            _cacheService = cacheService;
            _tokenUser = tokenUser;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteDesignationCommand command, CancellationToken cancellationToken)
        {

            int companyId = int.Parse(_tokenUser.GetUserCompanyId());

            var designation = await _designationRepository.DesignationGetDataAsync(command.designationId);

            if (designation == null)
            {
                return Errors.Common.DataNotFound;
            }

            bool isInUse = await _designationRepository.IsDesignationInUseAsync(command.designationId);
            if (isInUse)
            {
                return Errors.Common.IsUsed("Designation");
            }

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Designation>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return await _designationRepository.DeleteDesignationAsync(designation);
        }
    }
}
