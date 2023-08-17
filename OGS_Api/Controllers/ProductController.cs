namespace Ogs_Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OGS_Api.Data;
    using OGS_Api.Repositories;
    using OGS_Api.Services;
    using OGS_Api.DTO;
    using AutoMapper;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository _productRepository;
        public readonly IPhotoService _photoService;
        public readonly OgsContext _context;
        public readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IPhotoService photoService, OgsContext context, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._photoService = photoService;
            this._context = context;
            this._mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var Products = await _productRepository.GetAllProductAsync();
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]int id){
            var product = await _productRepository.GetProductByIdAsync(id);
            return Ok(product);
        }

        // [HttpPost]
        // public async Task<IActionResult> AddProduct([FromBody]Product product){
        //     var result = await _productRepository.AddProductAsync(product);
        //     return Ok(result);
        // }

        [HttpPost("photo/{productId}")]
        public async Task<IActionResult> AddProductPhoto(IFormFile file, int productId){

            var result = await _photoService.UploadImageAsync(file);

            var product = await _productRepository.GetProductByIdAsync(productId);

            product.imageUrl = result.SecureUrl.AbsoluteUri;
            product.publicId = result.PublicId;

            await _context.SaveChangesAsync();

            return Ok(201);
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct2([FromForm] ProductFileModel fileModel){

            var result = await _photoService.UploadImageAsync(fileModel.file);

            fileModel.imageUrl = result.SecureUrl.AbsoluteUri;
            fileModel.publicId = result.PublicId;

            var product = _mapper.Map<Product>(fileModel);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromForm] ProductFileModel fileModel){

            var result = await _photoService.UploadImageAsync(fileModel.file);

            fileModel.imageUrl = result.SecureUrl.AbsoluteUri;
            fileModel.publicId = result.PublicId;

            var updatedProduct = _mapper.Map<Product>(fileModel);

            var product = await _context.Products.FindAsync(productId);

            if(product != null){
                
                product.Name = updatedProduct.Name;
                product.desc = updatedProduct.desc;
                product.price = updatedProduct.price;
                product.quantity = updatedProduct.quantity;
                product.categoryId = updatedProduct.categoryId;
                product.imageUrl = updatedProduct.imageUrl;
                product.publicId = updatedProduct.publicId;

                 await _context.SaveChangesAsync();
            }

            return Ok(product);
        }

        [HttpDelete("delete/{productId}")]

        public async Task<IActionResult> DeleteCategoryById([FromRoute] int productId){

            var result = await _productRepository.DeleteProductByIdAsync(productId);
            return Ok(result);
        }
    }
}