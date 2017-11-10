using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding.Config
{
    public class KeyVaultEncryptorAsyncConverter : IConverter<KeyVaultEncryptorAttribute, IEncryptorAsync>
    {
        private readonly IKeyVaultProviderFactory _keyVaultProviderFactory;

        public KeyVaultEncryptorAsyncConverter(IKeyVaultProviderFactory keyVaultProviderFactory)
        {
            _keyVaultProviderFactory = keyVaultProviderFactory;
        }

        public IEncryptorAsync Convert(KeyVaultEncryptorAttribute keyVaultEncryptorAttribute)
        {
            return new EncryptorAsync(_keyVaultProviderFactory.GetKeyVaultProvider(keyVaultEncryptorAttribute), keyVaultEncryptorAttribute);
        }
    }
}