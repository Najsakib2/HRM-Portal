using AutoMapper;
using HRM.Applicatin.Common.Exceptions;
using HRM.Applicatin.IRepository;
using HRM.Applicatin.IRepository.Authentication;
using HRM.Application;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HRM.Applicatin
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, QueryUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IJwtTokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;


        public SignInQueryHandler(IUserRepository userRepository, IPasswordHasher<Users> passwordHasher, IJwtTokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IUserDetailsRepository userDetailsRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public async Task<QueryUserDto> Handle(SignInQuery query, CancellationToken cancellationToken)
        {

            var user = await _userRepository.UserGetDataByEmailAsync(query.email);

            if (user == null)
                throw new NotFoundException("User not found");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, query.password);

            if (result != PasswordVerificationResult.Success)
               throw new UnauthorizedException("Invalid credentials");

            var jwt = _tokenService.GenerateAccessToken(user);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtId = jwtHandler.ReadJwtToken(jwt).Id;
            var refreshToken = _tokenService.GenerateRefreshToken(user.ID, jwtId);

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);

            var userDetails = await _userDetailsRepository.UserDetailsGetDataByUserIDAsync(user.ID);

            var userMaping = _mapper.Map<QueryUserDto>(user);
            userMaping.JwtToken = jwt;
            userMaping.RefreshToken = refreshToken.Token;
            userMaping.UserDetails = userDetails != null ? _mapper.Map<UserDetailsDto>(userDetails) : null;

            return userMaping;
        }
    }
}
