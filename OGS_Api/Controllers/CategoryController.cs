namespace OGS_Api.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OGS_Api.Data;
    using OGS_Api.DTO;
    using OGS_Api.Repositories;
    using OGS_Api.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IPhotoService _photoService;
        public readonly OgsContext _context;
        public readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IPhotoService photoService, OgsContext context, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._photoService = photoService;
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]int id){
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        // [HttpPost]
        // public async Task<IActionResult> AddCategory([FromBody]Categories category){
        //     var result = await _categoryRepository.AddCategoryAsync(category);
        //     return Ok(result);
        // }

        [HttpPost("photo/{categoryId}")]
        public async Task<IActionResult> AddCategoryPhoto(IFormFile file, int categoryId){

            var result = await _photoService.UploadImageAsync(file);

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            category.imageUrl = result.SecureUrl.AbsoluteUri;
            category.publicId = result.PublicId;

            await _context.SaveChangesAsync();

            return Ok(201);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory2([FromForm] CategoryFileModel fileModel){

            var result = await _photoService.UploadImageAsync(fileModel.file);

            fileModel.imageUrl = result.SecureUrl.AbsoluteUri;
            fileModel.publicId = result.PublicId;

            var category = _mapper.Map<Categories>(fileModel);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromForm] CategoryFileModel fileModel){

            var result = await _photoService.UploadImageAsync(fileModel.file);

            fileModel.imageUrl = result.SecureUrl.AbsoluteUri;
            fileModel.publicId = result.PublicId;

            var updatedCategory = _mapper.Map<Categories>(fileModel);

            var category = await _context.Categories.FindAsync(categoryId);

            if(category != null){
                
                category.Name = updatedCategory.Name;
                category.imageUrl = updatedCategory.imageUrl;
                category.publicId = updatedCategory.publicId;

                 await _context.SaveChangesAsync();
            }

            return Ok(category);
        }


        [HttpDelete("delete/{categoryId}")]

        public async Task<IActionResult> DeleteCategoryById([FromRoute] int categoryId){

            var result = await _categoryRepository.DeleteCategoryByIdAsync(categoryId);
            return Ok(result);
        }
    }
}