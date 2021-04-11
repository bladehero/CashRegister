using System.IO;

namespace CashRegister.Models.Services
{
    public class ProductSM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BarcodeId { get; set; }
        public string Barcode { get; set; }
        
        public int PictureId { get; set; }
        public string PicturePath { get; set; }
        public string PictureFullPath => Path.Combine(Directory.GetCurrentDirectory(), "Images", PicturePath ?? string.Empty);
    }
}