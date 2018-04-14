using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using CarFunction;
using CarFunction.Model;

using Newtonsoft.Json;

namespace CarFunction.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestGetMethod()
        {
            TestLambdaContext context;
            APIGatewayProxyRequest request;
            APIGatewayProxyResponse response;

            Functions functions = new Functions();

            var car = new Model.Car() {Model = "Passat", Mileage = 150000, Make = "VW", Year = 1999};

            request = new APIGatewayProxyRequest();
            context = new TestLambdaContext();
            response = functions.Get(request, context);
            Assert.Equal(200, response.StatusCode);
            string result = JsonConvert.SerializeObject(car);
            Assert.Equal(result, response.Body);
        }
    }
}
