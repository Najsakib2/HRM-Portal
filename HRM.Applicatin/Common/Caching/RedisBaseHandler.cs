//using ErrorOr;
//using HRM.Applicatin.Service;
//using HRM.Domain.Common.Errors;

//namespace HRM.Application.Common.Caching
//{
//    public class BaseHandler
//    {
//        protected readonly IRedisCacheService _cacheService;

//        public BaseHandler(IRedisCacheService cacheService)
//        {
//            _cacheService = cacheService;
//        }

//        protected async Task<ErrorOr<T>> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan? expiration = null)
//        {
//            var cachedData = await _cacheService.GetAsync<T>(cacheKey);
//            if (cachedData != null) return cachedData;

//            var data = await getDataFunc();
//            if (data == null) return Errors.Common.DataNotFound;

//            await _cacheService.SetAsync(cacheKey, data, expiration ?? TimeSpan.FromMinutes(5));
//            return data;
//        }

//        protected async Task InvalidateCacheAsync(params string[] keys)
//        {
//            foreach (var key in keys)
//            {
//                await _cacheService.RemoveAsync(key);
//            }
//        }
//    }
//}
