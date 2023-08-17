using System;
using OGS_Api.Data;
using Microsoft.AspNetCore.Http;

namespace OGS_Api.DTO
{
    public class ProductFileModel
    {
        public string Name { get; set; }

         public IFormFile file { get; set; }
        
        public string desc { get; set; }
        
        public double price { get; set; }

        public int quantity { get; set; }

        public int categoryId { get; set; }

        public Categories category { get; set; }
        
        public string imageUrl { get; set; }
        
        public string publicId { get; set; }
    }
}