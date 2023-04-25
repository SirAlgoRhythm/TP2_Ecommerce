namespace StatsAPI.Models
{
    public class StatistiqueClient
    {
        public Guid StatistiqueClientId { get; set; }
        public Double TotalCashSpent { get; set; }
        public int TotalArticleBought { get; set; }

        //FK avec un autre service
        public Guid UserId { get; set; }
    }
}
