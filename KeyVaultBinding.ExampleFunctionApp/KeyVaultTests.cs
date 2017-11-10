using System.Net;
using System.Net.Http;
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
            [KeyVaultEncryptor("AKey")] IEncryptorAsync encryptorAsync,
            TraceWriter log)
        {
            var cipher = await encryptorAsync.EncryptAsync(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(await encryptorAsync.DecryptAsync(cipher));
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }

        [FunctionName("EncryptDecrypt")]
        public static HttpResponseMessage EncryptDecrypt(
            [HttpTrigger("GET")] HttpRequestMessage request,
            [KeyVaultEncryptor("AKey")] IEncryptor encryptor,
            TraceWriter log)
        {
            var cipher = encryptor.Encrypt(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(encryptor.Decrypt(cipher));
            return request.CreateResponse(HttpStatusCode.OK, $"{check}");
        }
    }
}
