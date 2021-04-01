namespace CashRegister.Models.Services
{
    public class OrderProductSM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public ProductSM Product { get; set; }
        public OrderSM Order { get; set; }
    }
}