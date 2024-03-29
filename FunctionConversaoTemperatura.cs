using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ConversaoTemperatura
{
    public class FunctionConversaoTemperatura
    {
        private readonly ILogger<FunctionConversaoTemperatura> _logger;

        public FunctionConversaoTemperatura(ILogger<FunctionConversaoTemperatura > log)
        {
            _logger = log;
        }

        [FunctionName("ConverterFahrenheitParaCelsius")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "fahrenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **fahrenheit** para conversão em celsius")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retornar o valor em Celsius")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterFahrenheitParaCelsius/{fahrenheit}")] HttpRequest req,
            double fahrenheit)
        {
            _logger.LogInformation($"Parametro recebido:{fahrenheit}", fahrenheit);

            var valorEmCelsius = (fahrenheit - 32) * 5 / 9;

            string responseMessage = $"O valor em fahrenheit {fahrenheit} em celsius é: {valorEmCelsius}";

            _logger.LogInformation($"Conversão efetuada. Resultado: {valorEmCelsius}");

            return new OkObjectResult(responseMessage);


        
        }
    }
}

