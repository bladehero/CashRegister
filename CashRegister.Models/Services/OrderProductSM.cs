namespace CashRegister.Models.Services
{
    public class OrderProductSM
    {
        public OrderProductSM(ProductSM product)
        {
            Product = product;
        }

        public int Id { get; set; }
        public int Quantity { get; set; } = 1;

        public ProductSM Product { get; set; }
        public OrderSM Order { get; set; }
        
        public decimal TotalPrice => Quantity * Product.Price;
    }
}