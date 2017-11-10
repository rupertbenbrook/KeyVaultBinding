using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace KeyVaultBinding.Config
{
    public class KeyVaultProviderFactory : IKeyVaultProviderFactory
    {
        public IKeyVaultProvider GetKeyVaultProvider(KeyVaultAttribute keyVaultAttribute)
        {
            // TODO: Also need to support certificate login with client Id
            if (keyVaultAttribute.ClientId == null)
            {
                return new KeyVaultProvider(keyVaultAttribute.BaseUrl,
                    new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
            }
            return new KeyVaultProvider(keyVaultAttribute.BaseUrl,
                new AccessTokenCache(new ClientCredential(
                    keyVaultAttribute.ClientId,
                    keyVaultAttribute.ClientSecret)).GetTokenAsync);
        }
    }
}