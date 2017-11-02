using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace KeyVaultBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class KeyVaultAttribute : Attribute
    {
        [AppSetting(Default = "KeyVaultBaseUrl")]
        public string BaseUrl { get; set; }

        [AppSetting(Default = "KeyVaultClientId")]
        public string ClientId { get; set; }

        [AppSetting(Default = "KeyVaultClientSecret")]
        public string ClientSecret { get; set; }

        [AutoResolve]
        public string SecretName { get; set; }

        [AutoResolve]
        public string SecretVersion { get; set; }
    }
}