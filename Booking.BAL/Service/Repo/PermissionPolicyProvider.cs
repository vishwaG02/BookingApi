using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.BAL.Service.Repo
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly IMemoryCache _memoryCache;
        public PermissionPolicyProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private readonly AuthorizationOptions _defaultOptions = new AuthorizationOptions();

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(_defaultOptions.DefaultPolicy);

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            Task.FromResult<AuthorizationPolicy?>(null);

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            return _memoryCache.GetOrCreateAsync(policyName, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(1);
                return Task.FromResult(
                    new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .RequireClaim(GlobalConstant.PermissionClainName, policyName)
                        .Build());
            });

        }
    }
}
