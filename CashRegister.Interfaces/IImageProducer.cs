using System.Drawing;

namespace CashRegister.Interfaces
{
    public interface IImageProducer
    {
        Image GetImage(string path);
    }
}