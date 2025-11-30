
using HRM.Applicatin.Service;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace HRM.Infrastructure.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        //private readonly StackExchange.Redis.IDatabase _database;
        //private readonly string _instanceName;

        //public RedisCacheService(IConnectionMultiplexer redis, IConfiguration configuration)
        //{
        //    _database = redis.GetDatabase();
        //    _instanceName = configuration["Redis:InstanceName"] ?? "DefaultCache";
        //}

        //public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        //{
        //    var jsonData = JsonSerializer.Serialize(value);
        //    await _database.StringSetAsync($"{_instanceName}:{key}", jsonData, expiry);
        //}

        //public async Task<T?> GetAsync<T>(string key)
        //{
        //    var value = await _database.StringGetAsync($"{_instanceName}:{key}");
        //    return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default;
        //}

        //public async Task<bool> RemoveAsync(string key)
        //{
        //    return await _database.KeyDeleteAsync($"{_instanceName}:{key}");
        //}

        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiration);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }
    }
}
