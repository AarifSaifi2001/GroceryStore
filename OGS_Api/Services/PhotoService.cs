using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace OGS_Api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IConfiguration config)
        {
            Account account = new Account(
                config.GetSection("CloudinarySettings:CloudName").Value,
                config.GetSection("CloudinarySettings:ApiKey").Value,
                config.GetSection("CloudinarySettings:ApiSecret").Value
            );

            cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeletePhotoAsync(string photoPublicId)
        {
            var deleteParams = new DeletionParams(photoPublicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task<ImageUploadResult> UploadImage2Async(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();

            if(photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation().Height(252).Width(300)
                };
                
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();

            if(photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation().Height(252).Width(300)
                };
                
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;  
        }
    }
}