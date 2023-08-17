using Microsoft.AspNetCore.Http;

namespace OGS_Api.DTO
{
    public class UserFileModel{
        
        public IFormFile file { get; set; }

        public string imageUrl { get; set; }

        public string publicId { get; set;}
    }
}