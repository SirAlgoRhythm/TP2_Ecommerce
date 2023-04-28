using System.ComponentModel.DataAnnotations.Schema;

namespace PanierAPI.Models
{
    //On veut seulement stocker la liste des IDs
    public class ProduitIds
    {
        public Guid ProduitId { get; set; }
    }
}
