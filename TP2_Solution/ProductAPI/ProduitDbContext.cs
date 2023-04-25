using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI
{
    public class ProduitDbContext: DbContext
    {
        public DbSet<Produit> Produits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //Antoine connection string:
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string db_name = "TP2_ProductAPI_DB";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={db_name}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>()
                .HasKey(e => e.ProduitId);
        }
    }
}
