using Microsoft.Azure.WebJobs.Host.Config;

namespace KeyVaultBinding.Config
{
    public class KeyVaultBindingExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var factory = new KeyVaultProviderFactory();
            context.AddBindingRule<KeyVaultAttribute>()
                .BindToInput(new KeyVaultSecretStringAsyncConverter(factory));
        }
    }
}