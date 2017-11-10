using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface IKeyVaultProvider
    {
        Task<string> GetSecret(string name, string version, CancellationToken cancellationToken);

        Task<byte[]> Encrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken);

        Task<byte[]> Decrypt(string keyName, string keyVersion, string algorithm,
            byte[] value, CancellationToken cancellationToken);
    }
}