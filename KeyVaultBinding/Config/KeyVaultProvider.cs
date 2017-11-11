using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace KeyVaultBinding.Config
{
    public class KeyVaultProvider : IKeyVaultProvider
    {
        private readonly string _baseUrl;
        private readonly KeyVaultClient _keyVaultClient;

        public KeyVaultProvider(string baseUrl, KeyVaultClient.AuthenticationCallback authenticationCallback)
        {
            _baseUrl = baseUrl;
            _keyVaultClient = new KeyVaultClient(authenticationCallback);
        }

        public async Task<string> GetSecret(string secretName, string secretVersion, CancellationToken cancellationToken)
        {
            var secret = await _keyVaultClient.GetSecretAsync(
                _baseUrl,
                secretName,
                secretVersion ?? string.Empty,
                cancellationToken);
            return secret.Value;
        }

        public async Task<byte[]> Encrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken)
        {
            var encrypt = await _keyVaultClient.EncryptAsync(_baseUrl, keyName, keyVersion, algorithm, value, cancellationToken);
            return encrypt.Result;
        }
        public async Task<byte[]> Decrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken)
        {
            var decrypt = await _keyVaultClient.DecryptAsync(_baseUrl, keyName, keyVersion, algorithm, value, cancellationToken);
            return decrypt.Result;
        }

        public async Task<byte[]> Sign(string keyName, string keyVersion, string algorithm, byte[] digest, CancellationToken cancellationToken)
        {
            var sign = await _keyVaultClient.SignAsync(_baseUrl, keyName, keyVersion, algorithm, digest, cancellationToken);
            return sign.Result;
        }

        public async Task<bool> Verify(string keyName, string keyVersion, string algorithm, byte[] digest, byte[] signature,
            CancellationToken cancellationToken)
        {
            var sign = await _keyVaultClient.VerifyAsync(_baseUrl, keyName, keyVersion, algorithm, digest, signature, cancellationToken);
            return sign.Value != null && sign.Value.Value;
        }
    }
}