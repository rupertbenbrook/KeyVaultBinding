using Microsoft.Azure.WebJobs.Host.Config;

namespace KeyVaultBinding.Config
{
    public class KeyVaultBindingExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var factory = new KeyVaultProviderFactory();
            var secretBinding = context.AddBindingRule<KeyVaultSecretAttribute>();
            secretBinding
                .BindToInput(new KeyVaultSecretStringAsyncConverter(factory));

            var encryptorBinding = context.AddBindingRule<KeyVaultEncryptorAttribute>();
            encryptorBinding
                .BindToInput(new KeyVaultEncryptorAsyncConverter(factory));
            encryptorBinding
                .BindToInput(new KeyVaultEncryptorConverter(factory));
        }
    }
}