using System;

namespace OGS_Api.Data
{
    public class Categories{

        public int id { get; set; }
        
        public string Name { get; set; }
        
        public string imageUrl { get; set; }

        public string publicId { get; set;}
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        
    }
}