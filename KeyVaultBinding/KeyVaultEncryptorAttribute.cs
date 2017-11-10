using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace KeyVaultBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class KeyVaultEncryptorAttribute : KeyVaultAttribute
    {
        public const string DefaultAlgorithm = "RSA-OAEP";

        public KeyVaultEncryptorAttribute(string keyName)
        {
            KeyName = keyName;
            Algorithm = DefaultAlgorithm;
        }

        [AutoResolve]
        public string KeyName { get; set; }

        [AutoResolve]
        public string KeyVersion { get; set; }

        [AutoResolve]
        public string Algorithm { get; set; }
    }
}