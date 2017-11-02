using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace KeyVaultBinding.Config
{
    public class TokenCache
    {
        private readonly ClientCredential _clientCredential;
        private static AuthenticationResult _cachedResult;

        public TokenCache(ClientCredential clientCredential)
        {
            _clientCredential = clientCredential;
        }

        public async Task<string> GetToken(string authority, string resource, string scope)
        {
            if (_cachedResult != null && _cachedResult.ExpiresOn > DateTimeOffset.UtcNow)
            {
                return _cachedResult.AccessToken;
            }
            var result = await new AuthenticationContext(authority).AcquireTokenAsync(resource,
                _clientCredential);
            _cachedResult = result ?? throw new InvalidOperationException("Failed to obtain the JWT token");
            return _cachedResult.AccessToken;
        }
    }
}