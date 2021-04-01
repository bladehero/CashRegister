namespace CashRegister.Models.Services
{
    public class ProductSM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string PicturePath { get; set; }
    }
}