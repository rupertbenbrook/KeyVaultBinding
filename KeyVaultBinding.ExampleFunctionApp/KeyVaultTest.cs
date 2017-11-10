using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KeyVaultBinding.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace KeyVaultBinding.ExampleFunctionApp
{
    public static class KeyVaultTest
    {
        [FunctionName("KeyVaultTest")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestMessage req,
            [KeyVaultSecret("ASecret")] string secretValue,
            [KeyVaultEncryptor("AKey")] IEncryptorAsync encryptorAsync,
            [KeyVaultEncryptor("AKey")] IEncryptor encryptor,
            TraceWriter log)
        {
            log.Info(secretValue);
            var cipher = await encryptorAsync.EncryptAsync(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(await encryptorAsync.DecryptAsync(cipher));
            var cipher2 = encryptor.Encrypt(Encoding.UTF8.GetBytes("this is more test plaintext"));
            var check2 = Encoding.UTF8.GetString(encryptor.Decrypt(cipher2));
            return req.CreateResponse(HttpStatusCode.OK, $"{secretValue}\r\n{check}\r\n{check2}");
        }
    }
}
