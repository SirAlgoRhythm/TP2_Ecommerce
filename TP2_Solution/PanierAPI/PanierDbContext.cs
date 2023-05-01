using Microsoft.EntityFrameworkCore;
using PanierAPI.Models;
using System.Text.RegularExpressions;

namespace PanierAPI
{
    public class PanierDbContext: DbContext
    {
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<ProduitIds> ProduitIds { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //Antoine connection string:
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string db_name = "TP2_PanierAPI_DB";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={db_name}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Panier>()
                .HasMany(p => p.ProduitIdListe)
                .WithMany()
                .UsingEntity(j => j.ToTable("PanierProduit"));
            modelBuilder.Entity<ProduitIds>()
                .HasKey(p => p.Id);
        }

    }
}
