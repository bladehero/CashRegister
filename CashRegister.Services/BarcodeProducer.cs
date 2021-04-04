using System.Drawing;
using BarcodeLib;
using CashRegister.Interfaces;

namespace CashRegister.Services
{
    public class BarcodeProducer : IBarcodeProducer
    {
        private static readonly Barcode _barcode = new Barcode();
        
        public Image GenerateImage(string barcode) => _barcode.Encode(TYPE.UPCA, barcode);
    }
}