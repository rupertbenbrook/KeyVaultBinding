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
            [KeyVaultEncryptor("AKey")] IEncryptorAsync encryptor,
            TraceWriter log)
        {
            log.Info(secretValue);
            var cipher = await encryptor.Encrypt(Encoding.UTF8.GetBytes("this is test plaintext"));
            var check = Encoding.UTF8.GetString(await encryptor.Decrypt(cipher));
            return req.CreateResponse(HttpStatusCode.OK, $"{secretValue}\r\n{check}");
        }
    }
}
