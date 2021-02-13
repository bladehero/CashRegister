using System.Collections.Generic;

namespace CashRegister.Models
{
    public class Product
    {
        public Product()
        {
            OrderProducts = new List<OrderProduct>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Barcode Barcode { get; set; }
        public Picture Picture { get; set; }
        public IList<OrderProduct> OrderProducts { get; set; }
    }
}