using Demo.SignalR.Web.Models;

namespace Demo.SignalR.Web.ProdcutService
{
    public interface IProductService
    {
       List<Product> Products { get; set; }

       Task GetProductsAsync();

       Task<Product?> GetProductAsync(Guid id);
       Task AddProductAsync(Product product);

       Task UpdateProductAsync(Product product);

    }
}