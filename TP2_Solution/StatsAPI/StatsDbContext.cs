using Microsoft.EntityFrameworkCore;
using StatsAPI.Models;

namespace StatsAPI
{
    public class StatsDbContext: DbContext
    {
        public DbSet<StatistiqueClient> StatistiqueClients { get; set; }
        public DbSet<StatistiqueVendeur> StatistiqueVendeurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //Antoine connection string:
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string db_name = "TP2_StatsAPI_DB";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={db_name}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatistiqueClient>()
                .HasKey(e => e.UtilisateurId);

            modelBuilder.Entity<StatistiqueVendeur>()
                .HasKey(e => e.UtilisateurId);
        }
    }
}
