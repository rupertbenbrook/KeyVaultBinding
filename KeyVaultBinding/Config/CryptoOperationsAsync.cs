using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public class CryptoOperationsAsync : ICryptoOperationsAsync, ICryptoOperations
    {
        private readonly IKeyVaultProvider _keyVaultProvider;
        private readonly KeyVaultCryptoAttribute _keyVaultCryptoAttribute;

        public CryptoOperationsAsync(IKeyVaultProvider keyVaultProvider, KeyVaultCryptoAttribute keyVaultCryptoAttribute)
        {
            _keyVaultProvider = keyVaultProvider;
            _keyVaultCryptoAttribute = keyVaultCryptoAttribute;
        }

        public Task<byte[]> EncryptAsync(byte[] plain)
        {
            return EncryptAsync(plain, CancellationToken.None);
        }

        public Task<byte[]> EncryptAsync(byte[] plain, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Encrypt(
                _keyVaultCryptoAttribute.KeyName,
                _keyVaultCryptoAttribute.KeyVersion ?? string.Empty,
                _keyVaultCryptoAttribute.Algorithm,
                plain,
                cancellationToken);
        }

        public byte[] Encrypt(byte[] plain)
        {
            return EncryptAsync(plain).Result;
        }

        public Task<byte[]> DecryptAsync(byte[] ciper)
        {
            return DecryptAsync(ciper, CancellationToken.None);
        }

        public Task<byte[]> DecryptAsync(byte[] ciper, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Decrypt(
                _keyVaultCryptoAttribute.KeyName,
                _keyVaultCryptoAttribute.KeyVersion ?? string.Empty,
                _keyVaultCryptoAttribute.Algorithm,
                ciper,
                cancellationToken);
        }

        public byte[] Decrypt(byte[] cipher)
        {
            return DecryptAsync(cipher).Result;
        }

        public Task<byte[]> SignAsync(byte[] digest)
        {
            return SignAsync(digest, CancellationToken.None);
        }

        public Task<byte[]> SignAsync(byte[] digest, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Sign(
                _keyVaultCryptoAttribute.KeyName,
                _keyVaultCryptoAttribute.KeyVersion ?? string.Empty,
                _keyVaultCryptoAttribute.Algorithm,
                digest,
                cancellationToken);
        }

        public byte[] Sign(byte[] digest)
        {
            return SignAsync(digest).Result;
        }

        public Task<bool> VerifyAsync(byte[] digest, byte[] signature)
        {
            return VerifyAsync(digest, signature, CancellationToken.None);
        }

        public Task<bool> VerifyAsync(byte[] digest, byte[] signature, CancellationToken cancellationToken)
        {
            return _keyVaultProvider.Verify(
                _keyVaultCryptoAttribute.KeyName,
                _keyVaultCryptoAttribute.KeyVersion ?? string.Empty,
                _keyVaultCryptoAttribute.Algorithm,
                digest,
                signature,
                cancellationToken);
        }

        public bool Verify(byte[] digest, byte[] signature)
        {
            return VerifyAsync(digest, signature).Result;
        }
    }
}