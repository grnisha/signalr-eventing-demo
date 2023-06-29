using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Demo.SignalR.Shared;

namespace Demo.SignalR.Api
{
    public static class SignalRDemo
    {
      
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, Route = "demoHub/negotiate")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "demoHub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public static async Task Broadcast([CosmosDBTrigger(
            databaseName: "demodb",
            containerName: "product",
            Connection  = "CosmosDBConnectionSetting",
            LeaseContainerName  = "leases", 
            FeedPollDelay = 500,
            CreateLeaseContainerIfNotExists  = true)] IReadOnlyList<Product> input,   
            [CosmosDB(
                databaseName: "demodb",
                containerName: "product",
                Connection = "CosmosDBConnectionSetting",
                SqlQuery = "SELECT * FROM c"
            )] IEnumerable<Product> products,   
           [SignalR(HubName = "demoHub")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (input != null && input.Count() > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);

                await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "ProductUpdates",
                    Arguments = new[] { products }
                });
            }
        }

    }
}