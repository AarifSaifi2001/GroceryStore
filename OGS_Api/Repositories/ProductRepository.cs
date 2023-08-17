using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OGS_Api.Data;

namespace OGS_Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly OgsContext _context;
        public ProductRepository(OgsContext context)
        {
            this._context = context;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.id;
        }

        public async Task<Product> DeleteProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
           var product = await _context.Products.FindAsync(id);
           return product;
        }
    }
}