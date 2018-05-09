using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CarFunction
{
    public class Functions
    {
        public void PrintParams(string parameterType, IDictionary<string, string> parameters)
        {
            if(parameters!=null)
            {
                foreach(string key in parameters.Keys)
                {
                    LambdaLogger.Log(parameterType + " key: " + key + " , value: " + parameters[key] + "\n");
                }
            }
            return;
        }
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {

            context.Logger.LogLine("Get Request\n");
            PrintParams("path", request.PathParameters);
            PrintParams("headers", request.Headers);
            PrintParams("query string", request.QueryStringParameters);
            PrintParams("stage variables", request.StageVariables);

            context.Logger.LogLine("path :" + request.Path + ", httpMethod: " + request.HttpMethod + ", resource: " + request.Resource);

            var car = new Model.Car() {Model = "Passat", Mileage = 150000, Make = "VW", Year = 1999};
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(car),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, { "Access-Control-Allow-Origin", "*" } }
            };

            return response;
        }

    }
}
