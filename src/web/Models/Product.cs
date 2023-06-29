using Newtonsoft.Json;
using System;

namespace Demo.SignalR.Web.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
