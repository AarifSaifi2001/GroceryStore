using System.Collections.Generic;
using System.Threading.Tasks;
using OGS_Api.Data;

namespace OGS_Api.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProductAsync();

        public Task<Product> GetProductByIdAsync(int id);

        public Task<int> AddProductAsync(Product product);
        
        public Task<Product> DeleteProductByIdAsync(int id);
    }
}