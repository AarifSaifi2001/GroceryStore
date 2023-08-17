using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OGS_Api.Data;
using OGS_Api.Services;

namespace OGS_Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly OgsContext _context;
        public readonly IPhotoService _photoService;
        public CategoryRepository(OgsContext context, IPhotoService photoService)
        {
            this._context = context;
            this._photoService = photoService;
        }

        public async Task<int> AddCategoryAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.id;
        }

        public async Task<Categories> DeleteCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Categories>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
           var category = await _context.Categories.FindAsync(id);
           return category;
        }
    }
}