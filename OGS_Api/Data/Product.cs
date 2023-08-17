using System;

namespace OGS_Api.Data
{
    public class Product
    {

        public int id { get; set; }
        
        public string Name { get; set; }
        
        public string desc { get; set; }
        
        public double price { get; set; }

        public int quantity { get; set; }

        public int categoryId { get; set; }
        public Categories category { get; set; }
        
        public string imageUrl { get; set; }
        
        public string publicId { get; set; }
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
           
    }
}