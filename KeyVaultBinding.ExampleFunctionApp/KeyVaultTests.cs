using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KeyVaultBinding.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace KeyVaultBinding.ExampleFunctionApp
{
    public static class KeyVaultTests
    {
        [FunctionName("GetSecret")]
        public static HttpResponseMessage GetSecret(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultSecret("ASecret")] string secretValue,
            TraceWriter log)
        {
            log.Info(secretValue);
            return request.CreateResponse(HttpStatusCode.OK, $"{secretValue}");
        }

        [FunctionName("EncryptDecryptAsync")]
        public static async Task<HttpResponseMessage> EncryptDecryptAsync(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultCrypto("AKey", "RSA-OAEP")] ICryptoOperationsAsync crypto,
            TraceWriter log)
        {
            var cipher = await crypto.EncryptAsync(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(await crypto.DecryptAsync(cipher));
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }

        [FunctionName("EncryptDecrypt")]
        public static HttpResponseMessage EncryptDecrypt(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultCrypto("AKey", "RSA-OAEP")] ICryptoOperations crypto,
            TraceWriter log)
        {
            var cipher = crypto.Encrypt(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(crypto.Decrypt(cipher));
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }

        [FunctionName("SignVerifyAsync")]
        public static async Task<HttpResponseMessage> SignVerifyAsync(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultCrypto("AKey", "RS256")] ICryptoOperationsAsync crypto,
            TraceWriter log)
        {
            var hasher = new SHA256CryptoServiceProvider();
            var digest = hasher.ComputeHash(Encoding.UTF8.GetBytes("this is test plaintext"));
            var sig = await crypto.SignAsync(digest);
            var check = await crypto.VerifyAsync(digest, sig);
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }

        [FunctionName("SignVerify")]
        public static HttpResponseMessage SignVerify(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultCrypto("AKey", "RS256")] ICryptoOperations crypto,
            TraceWriter log)
        {
            var hasher = new SHA256CryptoServiceProvider();
            var digest = hasher.ComputeHash(Encoding.UTF8.GetBytes("this is test plaintext"));
            var sig = crypto.Sign(digest);
            var check = crypto.Verify(digest, sig);
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }
    }
}
