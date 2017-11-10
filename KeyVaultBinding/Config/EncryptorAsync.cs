using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public class EncryptorAsync : IEncryptorAsync
    {
        private readonly IKeyVaultProvider _keyVaultProvider;
        private readonly KeyVaultEncryptorAttribute _keyVaultEncryptorAttribute;

        public EncryptorAsync(IKeyVaultProvider keyVaultProvider, KeyVaultEncryptorAttribute keyVaultEncryptorAttribute)
        {
            _keyVaultProvider = keyVaultProvider;
            _keyVaultEncryptorAttribute = keyVaultEncryptorAttribute;
        }

        public Task<byte[]> Encrypt(byte[] plainBytes)
        {
            return Encrypt(plainBytes, CancellationToken.None);
        }

        public Task<byte[]> Encrypt(byte[] plainBytes, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Encrypt(
                _keyVaultEncryptorAttribute.KeyName,
                _keyVaultEncryptorAttribute.KeyVersion ?? string.Empty,
                _keyVaultEncryptorAttribute.Algorithm,
                plainBytes,
                cancellationToken);
        }

        public Task<byte[]> Decrypt(byte[] cipherBytes)
        {
            return Decrypt(cipherBytes, CancellationToken.None);
        }

        public Task<byte[]> Decrypt(byte[] cipherBytes, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Decrypt(
                _keyVaultEncryptorAttribute.KeyName,
                _keyVaultEncryptorAttribute.KeyVersion ?? string.Empty,
                _keyVaultEncryptorAttribute.Algorithm,
                cipherBytes,
                cancellationToken);
        }
    }
}