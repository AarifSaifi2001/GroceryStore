using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OGS_Api.Data;

namespace OGS_Api.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Categories>> GetAllCategoriesAsync();

        public Task<Categories> GetCategoryByIdAsync(int id);

        public Task<int> AddCategoryAsync(Categories category);

        public Task<Categories> DeleteCategoryByIdAsync(int id);
    }
}