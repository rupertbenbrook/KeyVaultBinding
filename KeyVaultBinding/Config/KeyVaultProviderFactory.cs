using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace KeyVaultBinding.Config
{
    public class KeyVaultProviderFactory : IKeyVaultProviderFactory
    {
        public IKeyVaultProvider GetKeyVaultProvider(KeyVaultAttribute keyVaultAttribute)
        {
            if (keyVaultAttribute.ClientId == null)
            {
                return new KeyVaultProvider(keyVaultAttribute.BaseUrl,
                    new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
            }
            return new KeyVaultProvider(keyVaultAttribute.BaseUrl,
                new TokenCache(new ClientCredential(
                    keyVaultAttribute.ClientId,
                    keyVaultAttribute.ClientSecret)).GetToken);
        }
    }
}