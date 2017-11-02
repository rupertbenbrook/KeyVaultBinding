using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace KeyVaultBinding.Config
{
    public class KeyVaultSecretStringAsyncConverter : IAsyncConverter<KeyVaultAttribute, string>
    {
        public async Task<string> ConvertAsync(KeyVaultAttribute keyVaultAttribute, CancellationToken cancellationToken)
        {
            KeyVaultClient kv;
            if (keyVaultAttribute.ClientId == null)
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                kv = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            }
            else
            {
                var aquireToken = new TokenCache(new ClientCredential(
                    keyVaultAttribute.ClientId,
                    keyVaultAttribute.ClientSecret));
                kv = new KeyVaultClient(aquireToken.GetToken);
            }
            var sec = await kv.GetSecretAsync(
                keyVaultAttribute.BaseUrl,
                keyVaultAttribute.SecretName,
                keyVaultAttribute.SecretVersion ?? string.Empty,
                cancellationToken);
            return sec.Value;
        }
    }
}