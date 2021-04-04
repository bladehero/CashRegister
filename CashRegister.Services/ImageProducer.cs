using System.Drawing;
using CashRegister.Interfaces;

namespace CashRegister.Services
{
    public class ImageProducer : IImageProducer
    {
        public Image GetImage(string path) => Image.FromFile(path);
    }
}