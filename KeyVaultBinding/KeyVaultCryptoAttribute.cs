using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace KeyVaultBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class KeyVaultCryptoAttribute : KeyVaultAttribute
    {
        public KeyVaultCryptoAttribute(string keyName, string algorithm)
        {
            KeyName = keyName;
            Algorithm = algorithm;
        }

        [AutoResolve]
        public string KeyName { get; set; }

        [AutoResolve(Default = "")]
        public string KeyVersion { get; set; }

        [AutoResolve]
        public string Algorithm { get; set; }
    }
}