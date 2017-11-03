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
        public static Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestMessage req,
            [KeyVault(SecretName = "StorageConnectionString")] string storageConnectionString,
            [Blob("test/test.txt", Connection = "{storageConnectionString}")] string test,
            TraceWriter log)
        {
            log.Info(storageConnectionString);
            log.Info(test);
            return Task.FromResult(req.CreateResponse(HttpStatusCode.OK, $"{storageConnectionString}\r\n{test}"));
        }
    }
}
