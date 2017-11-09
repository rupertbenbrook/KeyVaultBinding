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
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestMessage req,
            [KeyVault(SecretName = "ASecret")] string secretValue,
            TraceWriter log)
        {
            log.Info(secretValue);
            return req.CreateResponse(HttpStatusCode.OK, $"{secretValue}");
        }
    }
}
