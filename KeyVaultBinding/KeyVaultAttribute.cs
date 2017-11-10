using System;
using Microsoft.Azure.WebJobs;

namespace KeyVaultBinding
{
    public abstract class KeyVaultAttribute : Attribute
    {
        [AppSetting(Default = "KeyVaultBaseUrl")]
        public string BaseUrl { get; set; }

        [AppSetting(Default = "KeyVaultClientId")]
        public string ClientId { get; set; }

        [AppSetting(Default = "KeyVaultClientSecret")]
        public string ClientSecret { get; set; }
    }
}