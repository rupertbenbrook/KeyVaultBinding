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

            var encryptorBinding = context.AddBindingRule<KeyVaultCryptoAttribute>();
            encryptorBinding
                .BindToInput(new KeyVaultCryptoOperationsAsyncConverter(factory));
            encryptorBinding
                .BindToInput(new KeyVaultCryptoOperationsConverter(factory));
        }
    }
}