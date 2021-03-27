using System.Threading;
using System.Threading.Tasks;
using CashRegister.Interfaces;
using CashRegister.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}