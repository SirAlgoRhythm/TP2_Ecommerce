namespace FactureAPI.Models
{
    public class Facture
    {
        //Clé primaire avec attributs nécessaires
        public Guid FactureId { get; set; }
        public DateTime DateTimeDay { get; set; }
        public double PrixTotal { get; set; }


        //FK liées à d'autres services
        public Guid UtilisateurId { get; set; }
        public Guid PanierId { get; set; }
    }
}
