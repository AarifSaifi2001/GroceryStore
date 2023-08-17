using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace OGS_Api.Services
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> UploadImageAsync(IFormFile photo);

         Task<ImageUploadResult> UploadImage2Async(IFormFile photo);
        //  Will add one more method to delete the photo 

         Task<DeletionResult> DeletePhotoAsync(string photoPublicId);
    }
}