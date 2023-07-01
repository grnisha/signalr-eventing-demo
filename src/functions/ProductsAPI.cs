using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Demo.SignalR.Models;

namespace Demo.SignalR.Api
{
    public static class ProductsAPI
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> GetProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req,
            [CosmosDB(
                databaseName: "demodb",
                containerName: "product",
                Connection = "CosmosDBConnectionSetting",
                SqlQuery = "SELECT * FROM c"
            )] IEnumerable<Product> products,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(products);
        }

        [FunctionName("GetProductById")]
        public static async Task<IActionResult> GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "demodb",
                containerName: "product",
                Connection = "CosmosDBConnectionSetting",
                Id = "{id}",
                PartitionKey = "{id}" 
            )] Product product,
            ILogger log)
        {
            log.LogInformation("Get Product By Id function processed a request.");

            return new OkObjectResult(product);
        }


        [FunctionName("AddProduct")]
        public static async Task<IActionResult> AddProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req,
            [CosmosDB(
            databaseName: "demodb",
            containerName: "product",
            Connection = "CosmosDBConnectionSetting"
        )] IAsyncCollector<Product> products,
            ILogger log)
        {
            log.LogInformation("Add Product function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);

            await products.AddAsync(product);
            log.LogInformation($"Prodcut {product.Id} - {product.Name} Created Successfully");

            return new OkObjectResult(product);
        }

        [FunctionName("EditProduct")]
        public static async Task<IActionResult> EditProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "product")] HttpRequest req,
            [CosmosDB(
            databaseName: "demodb",
            containerName: "product",
            Connection = "CosmosDBConnectionSetting"
        )] IAsyncCollector<Product> products,
            ILogger log)
        {
            log.LogInformation("Update Product function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);

            await products.AddAsync(product);
            log.LogInformation($"Prodcut {product.Id} - {product.Name} Updated Successfully");

            return new OkObjectResult(product);
        }


    }

}
