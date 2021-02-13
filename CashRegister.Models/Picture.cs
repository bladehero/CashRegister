using System.Drawing;

namespace CashRegister.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Path { get; set; }
        
        public Image Image => Image.FromFile(Path);
    }
}