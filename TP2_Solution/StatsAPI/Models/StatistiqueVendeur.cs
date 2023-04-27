namespace StatsAPI.Models
{
    public class StatistiqueVendeur
    {
        //Pk et Fk: UtilisateurId
        public Guid UtilisateurId { get; set; }
        public Double TotalCashReceved { get; set; }
        public Double Profit { get; set; }
        public int TotalArticleSold { get; set; }
    }
}
