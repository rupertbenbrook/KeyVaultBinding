using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface ICryptoOperationsAsync
    {
        Task<byte[]> EncryptAsync(byte[] plain);
        Task<byte[]> EncryptAsync(byte[] plain, CancellationToken cancellationToken);
        Task<byte[]> DecryptAsync(byte[] ciper);
        Task<byte[]> DecryptAsync(byte[] ciper, CancellationToken cancellationToken);
        Task<byte[]> SignAsync(byte[] digest);
        Task<byte[]> SignAsync(byte[] digest, CancellationToken cancellationToken);
        Task<bool> VerifyAsync(byte[] digest, byte[] signature);
        Task<bool> VerifyAsync(byte[] digest, byte[] signature, CancellationToken cancellationToken);
    }
}