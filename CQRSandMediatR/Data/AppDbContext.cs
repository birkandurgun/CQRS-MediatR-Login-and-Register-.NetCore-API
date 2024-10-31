using CQRSandMediatR.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSandMediatR.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BIRKAN\\SQLEXPRESS;Database=CQRSandMediatR;Trusted_Connection=True;TrustServerCertificate=true;");
        }
    }
}
