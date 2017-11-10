using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public class EncryptorAsync : IEncryptorAsync, IEncryptor
    {
        private readonly IKeyVaultProvider _keyVaultProvider;
        private readonly KeyVaultEncryptorAttribute _keyVaultEncryptorAttribute;

        public EncryptorAsync(IKeyVaultProvider keyVaultProvider, KeyVaultEncryptorAttribute keyVaultEncryptorAttribute)
        {
            _keyVaultProvider = keyVaultProvider;
            _keyVaultEncryptorAttribute = keyVaultEncryptorAttribute;
        }

        public Task<byte[]> EncryptAsync(byte[] plainBytes)
        {
            return EncryptAsync(plainBytes, CancellationToken.None);
        }

        public Task<byte[]> EncryptAsync(byte[] plainBytes, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Encrypt(
                _keyVaultEncryptorAttribute.KeyName,
                _keyVaultEncryptorAttribute.KeyVersion ?? string.Empty,
                _keyVaultEncryptorAttribute.Algorithm,
                plainBytes,
                cancellationToken);
        }

        public byte[] Encrypt(byte[] plainBytes)
        {
            return EncryptAsync(plainBytes).Result;
        }

        public Task<byte[]> DecryptAsync(byte[] cipherBytes)
        {
            return DecryptAsync(cipherBytes, CancellationToken.None);
        }

        public Task<byte[]> DecryptAsync(byte[] cipherBytes, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Decrypt(
                _keyVaultEncryptorAttribute.KeyName,
                _keyVaultEncryptorAttribute.KeyVersion ?? string.Empty,
                _keyVaultEncryptorAttribute.Algorithm,
                cipherBytes,
                cancellationToken);
        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            return DecryptAsync(cipherBytes).Result;
        }
    }
}