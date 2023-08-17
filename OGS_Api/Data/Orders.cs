using System;

namespace OGS_Api.Data
{
    public class Orders
    {
        public int id { get; set; }
        
        public string orderNumber { get; set; }
        
        public string productName { get; set; }

        public double productPrice { get; set;}

        public int productQuantity { get; set; }
        
        public string invoiceId { get; set; }

        public string orderstatus { get; set; }
        
        
        public int userId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}