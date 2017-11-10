using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace KeyVaultBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class KeyVaultEncryptorAttribute : KeyVaultAttribute
    {
        public KeyVaultEncryptorAttribute(string keyName)
        {
            KeyName = keyName;
        }

        [AutoResolve]
        public string KeyName { get; set; }

        [AutoResolve(Default = "")]
        public string KeyVersion { get; set; }

        [AutoResolve(Default = "RSA-OAEP")]
        public string Algorithm { get; set; }
    }
}