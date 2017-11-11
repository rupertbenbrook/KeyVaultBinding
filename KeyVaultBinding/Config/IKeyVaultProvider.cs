using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface IKeyVaultProvider
    {
        Task<string> GetSecret(string secretName, string secretVersion, CancellationToken cancellationToken);
        Task<byte[]> Encrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken);
        Task<byte[]> Decrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken);
        Task<byte[]> Sign(string keyName, string keyVersion, string algorithm,
            byte[] digest, CancellationToken cancellationToken);
        Task<bool> Verify(string keyName, string keyVersion, string algorithm,
            byte[] digest, byte[] signature, CancellationToken cancellationToken);
    }
}