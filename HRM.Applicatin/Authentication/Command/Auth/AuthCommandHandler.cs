using AutoMapper;
using HRM.Application.Common.Exceptions;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRM.Applicatin
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, CommandCompanyDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public SignUpCommandHandler(IUserRepository userRepository,IMapper mapper,IPasswordHasher<Users> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<CommandCompanyDto> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {

            var exist = await _userRepository.UserGetDataByEmailAsync(command.signUpDto.Email);

            if(exist != null)
            {
                throw new AlreadyExist("Email already exist");
            }

            var company = _mapper.Map<Company>(command.signUpDto);
            var user = _mapper.Map<Users>(command.signUpDto.UserDto);
            user.Password = _passwordHasher.HashPassword(user, command.signUpDto.UserDto.Password);

            await _userRepository.SignUpAsync(company,user);

            return command.signUpDto;
        }
    }
}
