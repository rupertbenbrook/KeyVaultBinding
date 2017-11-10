using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace KeyVaultBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class KeyVaultSecretAttribute : KeyVaultAttribute
    {
        public KeyVaultSecretAttribute(string secretName)
        {
            SecretName = secretName;
        }

        [AutoResolve]
        public string SecretName { get; set; }

        [AutoResolve(Default = "")]
        public string SecretVersion { get; set; }
    }
}