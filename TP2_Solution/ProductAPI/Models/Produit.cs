namespace ProductAPI.Models
{
    public class Produit
    {
        public Guid ProduitId { get; set; }
        public string Title { get; set; }
        public string Categorie { get; set; }
        public string Gender { get; set; }
        public Double Price { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public int Quantite { get; set; }
        public string ImageURL { get; set; }

        //Fk avec un autre service
        public Guid UtilisateurId { get; set; }
    }
}
