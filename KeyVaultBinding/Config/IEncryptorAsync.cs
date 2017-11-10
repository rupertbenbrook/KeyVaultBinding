using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface IEncryptorAsync
    {
        Task<byte[]> EncryptAsync(byte[] plainBytes);
        Task<byte[]> EncryptAsync(byte[] plainBytes, CancellationToken cancellationToken);
        Task<byte[]> DecryptAsync(byte[] cipherBytes);
        Task<byte[]> DecryptAsync(byte[] cipherBytes, CancellationToken cancellationToken);
    }
}