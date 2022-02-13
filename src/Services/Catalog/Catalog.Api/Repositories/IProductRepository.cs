using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(string Id);
        Task<IEnumerable<Product>> GetProductByNameAsync(string Name);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(string Category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string Id);
    }
}
