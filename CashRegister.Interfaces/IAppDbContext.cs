using System;
using System.Threading;
using System.Threading.Tasks;
using CashRegister.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Interfaces
{
    public interface IAppDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<Session> Sessions { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        DbSet<Barcode> Barcodes { get; set; }
        DbSet<Picture> Pictures { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
    }
}