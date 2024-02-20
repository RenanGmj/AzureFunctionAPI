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
    public class FunctionCelsiusParaFahrenheit
    {
        private readonly ILogger<FunctionCelsiusParaFahrenheit> _logger;

        public FunctionCelsiusParaFahrenheit(ILogger<FunctionCelsiusParaFahrenheit > log)
        {
            _logger = log;
        }

        
        
        [FunctionName("ConverterCelsiusParaFahrenheit")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "Celsius", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **Celsius** para conversão em Fahrenheit")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retornar o valor em Fahrenheit")]

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterCelsiusParaFahrenheit/{Celsius}")] HttpRequest req,
            double Celsius)
        {
            _logger.LogInformation($"Parametro recebido:{Celsius}", Celsius);

            var valorEmFahrenheit = (Celsius * 1.8) + 32;

            string responseMessage = $"O valor em Celsius {Celsius} em Fahrenheit é: {valorEmFahrenheit}";

            _logger.LogInformation($"Conversão efetuada. Resultado: {valorEmFahrenheit}");

            return new OkObjectResult(responseMessage);
        }
    }
}

