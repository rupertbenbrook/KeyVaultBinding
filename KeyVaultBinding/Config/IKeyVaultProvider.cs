using System.Threading;
using System.Threading.Tasks;

namespace KeyVaultBinding.Config
{
    public interface IKeyVaultProvider
    {
        Task<string> GetSecret(CancellationToken cancellationToken);
    }
}