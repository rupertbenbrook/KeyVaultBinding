using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding.Config
{
    public class KeyVaultCryptoOperationsConverter : IConverter<KeyVaultCryptoAttribute, ICryptoOperations>
    {
        private readonly IKeyVaultProviderFactory _keyVaultProviderFactory;

        public KeyVaultCryptoOperationsConverter(IKeyVaultProviderFactory keyVaultProviderFactory)
        {
            _keyVaultProviderFactory = keyVaultProviderFactory;
        }

        public ICryptoOperations Convert(KeyVaultCryptoAttribute keyVaultCryptoAttribute)
        {
            return new CryptoOperationsAsync(_keyVaultProviderFactory.GetKeyVaultProvider(keyVaultCryptoAttribute), keyVaultCryptoAttribute);
        }
    }
}