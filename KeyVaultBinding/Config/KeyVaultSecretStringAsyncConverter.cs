using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding.Config
{
    public class KeyVaultSecretStringAsyncConverter : IAsyncConverter<KeyVaultSecretAttribute, string>
    {
        private readonly IKeyVaultProviderFactory _keyVaultProviderFactory;

        public KeyVaultSecretStringAsyncConverter(IKeyVaultProviderFactory keyVaultProviderFactory)
        {
            _keyVaultProviderFactory = keyVaultProviderFactory;
        }

        public Task<string> ConvertAsync(KeyVaultSecretAttribute keyVaultAttribute, CancellationToken cancellationToken)
        {
            var provider = _keyVaultProviderFactory.GetKeyVaultProvider(keyVaultAttribute);
            return provider.GetSecret(keyVaultAttribute.SecretName, keyVaultAttribute.SecretVersion, cancellationToken);
        }
    }
}