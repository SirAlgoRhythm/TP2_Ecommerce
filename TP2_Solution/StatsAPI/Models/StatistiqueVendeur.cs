namespace StatsAPI.Models
{
    public class StatistiqueVendeur
    {
        public Guid StatistiqueVendeurId { get; set; }
        public Double TotalCashReceved { get; set; }
        public Double Profit { get; set; }
        public int TotalArticleSold { get; set; }

        //Fk avec un autre service
        public Guid UserId { get; set; }
    }
}
