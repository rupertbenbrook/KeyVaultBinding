using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            [KeyVault(SecretName = "StorageConnectionString")] string storageConnectionString,
            Binder binder,
            TraceWriter log)
        {
            log.Info(storageConnectionString);
            var content = await binder.BindAsync<string>(new BlobAttribute("test/test.txt") {Connection = storageConnectionString});
            log.Info(content);
            return req.CreateResponse(HttpStatusCode.OK, $"{storageConnectionString}\r\n{content}");
        }
    }
}
