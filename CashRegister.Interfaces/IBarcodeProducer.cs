using System.Drawing;

namespace CashRegister.Interfaces
{
    public interface IBarcodeProducer
    {
        Image GenerateImage(string barcode);
    }
}