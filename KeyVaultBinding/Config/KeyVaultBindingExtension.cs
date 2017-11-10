using Microsoft.Azure.WebJobs.Host.Config;

namespace KeyVaultBinding.Config
{
    public class KeyVaultBindingExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var factory = new KeyVaultProviderFactory();
            context.AddBindingRule<KeyVaultSecretAttribute>()
                .BindToInput(new KeyVaultSecretStringAsyncConverter(factory));
            context.AddBindingRule<KeyVaultEncryptorAttribute>()
                .BindToInput(new KeyVaultEncryptorAsyncConverter(factory));
        }
    }
}