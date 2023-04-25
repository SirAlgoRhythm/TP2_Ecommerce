using System.ComponentModel.DataAnnotations.Schema;

namespace PanierAPI.Models
{
    //On veut seulement pouvoir utiliser la classe, aucun produit n'y sera sauvegardé
    [NotMapped]
    public class Produit
    {
        public Guid ProduitId { get; set; }
        public string Title { get; set; }
        public string Categorie { get; set; }
        public string Gender { get; set; }
        public Double Price { get; set; }
        public string Vendor { get; set; }
        public Guid UtilisateurId { get; set; }
        public string Description { get; set; }
        public int Quantite { get; set; }
        public string ImageURL { get; set; }
    }
}
