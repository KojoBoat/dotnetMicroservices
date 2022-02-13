using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Exceptions;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductAsync(string Id)
        {
           return await _context.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, Name);
            return await _context.Products.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string Category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, Category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteProductAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrWhiteSpace(Id)) throw new ArgumentNullException("Id can\'t be null");
            FilterDefinition<Product> filter = Builders<Product>.Filter.Exists(p => p.Id == Id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var isUpdated = await _context
                                     .Products
                                     .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return isUpdated.IsAcknowledged
                    && isUpdated.ModifiedCount > 0; 
        }

        public async Task CreateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException("Prodcut can\'t be null!");
            await _context.Products.InsertOneAsync(product);
        }
    }
}
