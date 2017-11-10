using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace KeyVaultBinding.Config
{
    public class AccessTokenCache
    {
        private readonly ClientCredential _clientCredential;
        private static readonly ConcurrentDictionary<string, AuthenticationResult> TokenCache = new ConcurrentDictionary<string, AuthenticationResult>();

        public AccessTokenCache(ClientCredential clientCredential)
        {
            _clientCredential = clientCredential;
        }

        public async Task<string> GetTokenAsync(string authority, string resource, string scope)
        {
            var cacheKey = $"ClientId:{_clientCredential.ClientId};Authority:{authority};Resource={resource};Scope={scope};";
            if (TokenCache.TryGetValue(cacheKey, out var result))
            {
                if (result.ExpiresOn > DateTimeOffset.UtcNow)
                {
                    return result.AccessToken;
                }
            }
            result = await new AuthenticationContext(authority).AcquireTokenAsync(resource, _clientCredential);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }
            TokenCache.AddOrUpdate(cacheKey, result, (k, r) => result);
            return result.AccessToken;
        }
    }
}