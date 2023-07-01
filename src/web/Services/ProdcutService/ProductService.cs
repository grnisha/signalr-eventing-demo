using System.Net.Http.Json;
using Demo.SignalR.Web.Models;

namespace Demo.SignalR.Web.ProdcutService
{
    public class ProdcutService: IProductService
    {
        private readonly HttpClient _httpClient;

        public ProdcutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Product> Products { get ; set; } = new List<Product>();


        public async Task AddProductAsync(Product product)
        {
           await _httpClient.PostAsJsonAsync("api/product", product);
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/product/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            
            return await result.Content.ReadFromJsonAsync<Product>();
        }

        public async Task GetProductsAsync()
        {
            Products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products") ?? new List<Product>();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _httpClient.PutAsJsonAsync($"api/product", product);
        }
    }
}