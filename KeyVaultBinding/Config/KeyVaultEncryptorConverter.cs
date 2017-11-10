using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding.Config
{
    public class KeyVaultEncryptorConverter : IConverter<KeyVaultEncryptorAttribute, IEncryptor>
    {
        private readonly IKeyVaultProviderFactory _keyVaultProviderFactory;

        public KeyVaultEncryptorConverter(IKeyVaultProviderFactory keyVaultProviderFactory)
        {
            _keyVaultProviderFactory = keyVaultProviderFactory;
        }

        public IEncryptor Convert(KeyVaultEncryptorAttribute keyVaultEncryptorAttribute)
        {
            return new EncryptorAsync(_keyVaultProviderFactory.GetKeyVaultProvider(keyVaultEncryptorAttribute), keyVaultEncryptorAttribute);
        }
    }
}