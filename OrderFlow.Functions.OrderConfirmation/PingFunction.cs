using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OrderFlow.Functions.OrderConfirmation
{
    public class PingFunction
    {
        [Function("Ping")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteStringAsync("OrderFlow Functions is alive");
            return response;
        }
    }
}
