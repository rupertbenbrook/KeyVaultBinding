using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace KeyVaultBinding.Config
{
    public class KeyVaultProvider : IKeyVaultProvider
    {
        private readonly KeyVaultAttribute _keyVaultAttribute;
        private readonly KeyVaultClient _keyVaultClient;

        public KeyVaultProvider(KeyVaultAttribute keyVaultAttribute, KeyVaultClient.AuthenticationCallback authenticationCallback)
        {
            _keyVaultAttribute = keyVaultAttribute;
            _keyVaultClient = new KeyVaultClient(authenticationCallback);
        }

        public async Task<string> GetSecret(CancellationToken cancellationToken)
        {
            var secret = await _keyVaultClient.GetSecretAsync(
                _keyVaultAttribute.BaseUrl,
                _keyVaultAttribute.SecretName,
                _keyVaultAttribute.SecretVersion ?? string.Empty,
                cancellationToken);
            return secret.Value;
        }
    }
}