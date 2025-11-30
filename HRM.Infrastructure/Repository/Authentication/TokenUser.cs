
using HRM.Application.IRepository.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRM.Infrastructure.Repository.Authentication
{
    internal class TokenUser: ITokenUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User
                   ?? throw new InvalidOperationException("User is not authenticated.");
        }

        public string GetUserId()
        {
            return GetClaimValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserCompanyId()
        {
            return GetClaimValue("companyId");
        }

        public string GetUserName()
        {
            return GetClaimValue(ClaimTypes.Name);
        }

        public string GetUserFullName()
        {
            return GetClaimValue("FullName");
        }

        public string GetUserEmail()
        {
            return GetClaimValue(ClaimTypes.Email);
        }

        public string GetUserRole()
        {
            return GetClaimValue("role");
        }

        public bool IsActive()
        {
            var isActiveClaim = GetClaimValue("isActive");
            return bool.TryParse(isActiveClaim, out var result) && result;
        }

        private string GetClaimValue(string claimType)
        {
            var value = GetCurrentUser()?.FindFirst(claimType)?.Value;
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"{claimType} not found in token.");
            return value;
        }
    }
}
