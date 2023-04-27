namespace StatsAPI.Models
{
    public class StatistiqueClient
    {
        //Pk et Fk: UtilisateurId
        public Guid UtilisateurId { get; set; }
        public Double TotalCashSpent { get; set; }
        public int TotalArticleBought { get; set; }

    }
}
