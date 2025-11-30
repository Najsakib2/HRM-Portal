using HRM.Applicatin;
using HRM.Applicatin.Service;
using HRM.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class UserRepository: IUserRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly IRedisCacheService _cacheService;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public UserRepository(AppDbContext dbContext, IRedisCacheService cacheService,IPasswordHasher<Users> passwordHasher)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignUpAsync(Company company, Users user)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // 1. Create Company entity
                await _dbContext.Company.AddAsync(company);
                await _dbContext.SaveChangesAsync();

                // 2. Create User entity
                user.CompanyID = company.ID;
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Users> UpdateUserAsync(Users existingUser)
        {
            try
            {        
                 _dbContext.Entry(existingUser).State = EntityState.Modified;

                 _dbContext.Entry(existingUser).Property(x => x.Password).IsModified = false;

                 if (existingUser.userDetails != null)
                 {
                     _dbContext.Entry(existingUser.userDetails).State = EntityState.Modified;
                     _dbContext.Entry(existingUser.userDetails).Property(x => x.UserID).IsModified = false;
                 }

                 await _dbContext.SaveChangesAsync();
                 return existingUser;
            }
                
            catch (Exception)
            {
                throw;
            }           
        }

        public async Task<bool> DeleteUserAsync(Users user)
        {
            _dbContext.Users.Remove(user);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Users> UserGetDataAsync(int id)
        {
            try
            {
                var user = await _dbContext.Users.Include(u => u.Company).Include(u => u.userDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ID == id);
                return user;
            }

            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Users>> UserGetDataByCompanyID(int companyId)
        {
            var users = await _dbContext.Users
                .Where(u => u.CompanyID == companyId) 
                .Include(u => u.userDetails)
                    .ThenInclude(d => d.Department)
                .Include(u => u.userDetails)
                    .ThenInclude(d => d.Designation)
                .ToListAsync();
            return users;
        }

        public async Task<Users> UserGetDataByEmailAsync(string email)
        {
            var user = await _dbContext.Users
                .Include(x => x.Company)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<IEnumerable<Users>> UserGetAllDataAsync()
        {
            var users = await _dbContext.Users          
            .Include(u => u.userDetails)
                .ThenInclude(d => d.Department)
            .Include(u => u.userDetails.Designation)
            .ToListAsync();

            return users;
        }
    }
}
