
using HRM.Applicatin.IRepository;
using HRM.Applicatin.Service;
using HRM.Domain;
using HRM.Domain.Authentication;
using Microsoft.AspNetCore.Identity;

namespace HRM.Infrastructure.Repository
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                _dbContext.RefreshToken.Add(refreshToken);
                await _dbContext.SaveChangesAsync();
                return refreshToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
