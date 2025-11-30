
using HRM.Domain;
using HRM.Domain.Authentication;

namespace HRM.Applicatin.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken);
    }
}
