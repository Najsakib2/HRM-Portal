using AutoMapper;
using HRM.Applicatin.Service;
using HRM.Domain;
using HRM.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HRM.Test
{
    public class EmployeeRepositoryTests
    {
        private readonly Mock<IRedisCacheService> _mockCacheService;
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly UserRepository _repository;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "HRM_TestDB")
                .Options;

            _dbContext = new AppDbContext(options);
            _mockCacheService = new Mock<IRedisCacheService>();
            _repository = new UserRepository(_dbContext, _mockCacheService.Object,_passwordHasher);
        }

        [Fact]
        public async Task AddEmployeeAsync_Should_Add_To_Db_And_Set_Cache()
        {
            // Arrange
            var user = new Users
            {
                ID = 1,
                CompanyID = 1,
                FullName = "Test User",
                Email = "test@example.com",
                Password ="Mahfzu123@",
                IsActive = true,
                IsAdmin = false,
                JoinDate = DateTime.Now,
                EntryUserID = 1,
                EntryDate = DateTime.Now,
                UpdateUserID = 1,
                UpdateDate = DateTime.Now
            };

            // Act
            var result = await _repository.AddUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test User", result.FullName);
            Assert.Single(_dbContext.Users); // Make sure it's saved in DB
            _mockCacheService.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<Users>(), It.IsAny<TimeSpan>()), Times.Once);
            _mockCacheService.Verify(c => c.RemoveAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
