using AutoMapper;
using ErrorOr;
using HRM.Applicatin.Service;
using HRM.Application.Common.Exceptions;
using HRM.Application.IRepository.Authentication;
using HRM.Domain;
using HRM.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace HRM.Applicatin
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<Users>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IRedisCacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ITokenUser _tokenUser;

        public AddUserCommandHandler(IUserRepository userRepository, IPasswordHasher<Users> passwordHasher,IRedisCacheService redisCacheService, IMapper mapper,ITokenUser tokenUser)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _cacheService = redisCacheService;
            _mapper = mapper;
            _tokenUser = tokenUser; 
        }

        public async Task<ErrorOr<Users>> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {

            int companyId = int.Parse(_tokenUser.GetUserCompanyId());

            if (command.image != null)
            {
                var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Images");
                if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);

                var imagePath = Path.Combine(imageDir, command.image.FileName);
                using var imageStream = new FileStream(imagePath, FileMode.Create);
                await command.image.CopyToAsync(imageStream);

                command.user.userDetails.Image = command.image.FileName;
            }

            if (command.signature != null)
            {
                var sigDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Signatures");
                if (!Directory.Exists(sigDir)) Directory.CreateDirectory(sigDir);

                var sigPath = Path.Combine(sigDir, command.signature.FileName);
                using var sigStream = new FileStream(sigPath, FileMode.Create);
                await command.signature.CopyToAsync(sigStream);

                command.user.userDetails.Signature = command.signature.FileName;
            }

            var exist = await _userRepository.UserGetDataByEmailAsync(command.user.Email);

            if (exist != null)
            {
                throw new AlreadyExist("Email already exist");
            }

            command.user.Password = _passwordHasher.HashPassword(command.user, command.user.Password);

            var addedUser = await _userRepository.AddUserAsync(command.user);

            //var userDto = _mapper.Map<CommandUserDto>(addedUser);  // maping for save cach

            // Invalidate cache after insert
            string cacheKey = CacheKeyHelper<Users>.GetAllByCompany(companyId);
            await _cacheService.RemoveAsync(cacheKey);

            return addedUser;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Users>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _cacheService;
        private readonly IWebHostEnvironment _env;

        public UpdateUserCommandHandler(IUserRepository userRepository,IMapper mapper, IRedisCacheService redisCacheService,IWebHostEnvironment env)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = redisCacheService;
            _env = env;
        }

        public async Task<ErrorOr<Users>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            var userId = command.user.ID;

            //Handle Image
            if (command.image != null)
            {
                var imageDir = Path.Combine(_env.WebRootPath, "Uploads", "Images", userId.ToString());
                if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);

                var uniqueImageName = $"{Guid.NewGuid()}_{command.image.FileName}";
                var imagePath = Path.Combine(imageDir, uniqueImageName);

                using var imageStream = new FileStream(imagePath, FileMode.Create);
                await command.image.CopyToAsync(imageStream);

                // Save relative path in DB
                command.user.userDetails.Image = Path.Combine("Uploads", "Images", userId.ToString(), uniqueImageName).Replace("\\", "/");
            }

            //Handle Signature
            if (command.signature != null)
            {
                var sigDir = Path.Combine(_env.WebRootPath, "Uploads", "Signatures", userId.ToString());
                if (!Directory.Exists(sigDir)) Directory.CreateDirectory(sigDir);

                var uniqueSigName = $"{Guid.NewGuid()}_{command.signature.FileName}";
                var sigPath = Path.Combine(sigDir, uniqueSigName);

                using var sigStream = new FileStream(sigPath, FileMode.Create);
                await command.signature.CopyToAsync(sigStream);

                command.user.userDetails.Signature = Path.Combine("Uploads", "Signatures", userId.ToString(), uniqueSigName).Replace("\\", "/");
            }

            var existingUser = await _userRepository.UserGetDataAsync(command.user.ID);

            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {command.user.ID} not found.");

            var updatedUser = await _userRepository.UpdateUserAsync(command.user);

            string cacheKey = CacheKeyHelper<Users>.GetAllByCompany(command.user.CompanyID);
            await _cacheService.RemoveAsync(cacheKey);

            return _mapper.Map<Users>(updatedUser);
        }
    }

    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenUser _tokenUser;
        private readonly IRedisCacheService _cacheService;

        public DeleteUserCommandHandler(IUserRepository userRepository, ITokenUser tokenUser, IRedisCacheService cacheService)
        {
            _userRepository = userRepository;
            _tokenUser = tokenUser;
            _cacheService = cacheService;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.UserGetDataAsync(command.userId);
            if(user == null)
            {
                return Errors.Common.DataNotFound;
            }
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
