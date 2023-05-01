

namespace PanierAPI.Models
{
    public class Panier
    {
        public Guid PanierId { get; set; }
        public ICollection<ProduitIds> ProduitIdListe { get; set; }

        // Ajouter un jeton d'authentification pour savoir à qui est le panier???
    }
}
