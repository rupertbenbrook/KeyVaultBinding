using Microsoft.Azure.WebJobs.Host.Config;

namespace KeyVaultBinding.Config
{
    public class KeyVaultBindingExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<KeyVaultAttribute>()
                .BindToInput(new KeyVaultSecretStringAsyncConverter());
        }
    }
}