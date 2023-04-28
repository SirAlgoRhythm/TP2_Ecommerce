using Microsoft.EntityFrameworkCore;
using PaiementAPI.Models;

namespace PaiementAPI
{
    public class PaiementDbContext: DbContext
    {
        public DbSet<Paiement> Paiements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //Antoine connection string:
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string db_name = "TP2_PaiementAPI_DB";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={db_name}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paiement>()
                .HasKey(e => e.PaiementId);
        }
    }
}
