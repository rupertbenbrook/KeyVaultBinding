using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface IEncryptorAsync
    {
        Task<byte[]> Encrypt(byte[] plainBytes);
        Task<byte[]> Encrypt(byte[] plainBytes, CancellationToken cancellationToken);
        Task<byte[]> Decrypt(byte[] cipherBytes);
        Task<byte[]> Decrypt(byte[] cipherBytes, CancellationToken cancellationToken);
    }
}