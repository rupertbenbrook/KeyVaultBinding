using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding.Config
{
    public class KeyVaultCryptoOperationsAsyncConverter : IConverter<KeyVaultCryptoAttribute, ICryptoOperationsAsync>
    {
        private readonly IKeyVaultProviderFactory _keyVaultProviderFactory;

        public KeyVaultCryptoOperationsAsyncConverter(IKeyVaultProviderFactory keyVaultProviderFactory)
        {
            _keyVaultProviderFactory = keyVaultProviderFactory;
        }

        public ICryptoOperationsAsync Convert(KeyVaultCryptoAttribute keyVaultCryptoAttribute)
        {
            return new CryptoOperationsAsync(_keyVaultProviderFactory.GetKeyVaultProvider(keyVaultCryptoAttribute), keyVaultCryptoAttribute);
        }
    }
}