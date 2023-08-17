using System;

namespace OGS_Api.Data
{
    public class User
    {
        public int id { get; set; }
        
        public string username { get; set; }
        
        public string email { get; set; }
        
        public byte[] password { get; set; }
        
        public byte[] passwordKey { get; set; }
        
        public long mobileno { get; set; }
        
        public string address { get; set; }
        
        
        public string imageUrl { get; set; }
        
        public string publicId { get; set; }
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
    }
}